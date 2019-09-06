using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, ITakeDamage, IHaveStats
{
    [SerializeField] private int baseHealth = 100;

    [SerializeField] private float health;

    public static Player CurrentPlayer { get; private set; }
    public float Health
    {
        get
        {
            return health;
        }
    }
    private float _attack = 10, _defense = 10;
    private float _attackStatModifiersSum = 0;
    private float _attackStatModifiersMult = 1;
    private float _defenseStatModifiersSum = 0;
    private float _defenseStatModifiersMult = 1;

    public float Attack
    {
        get
        {
            return _attack * _attackStatModifiersMult + _attackStatModifiersSum;
        }

        private set
        {
            _attack = value;
        }
    }

    public float Defense
    {
        get
        {
            return _defense * _defenseStatModifiersMult + _defenseStatModifiersSum;
        }

        private set
        {
            _defense = value;
        }
    }

    //TODO change strings
    public void AddStatModifier(string stat, string modifierType, float value, float duration = 0f)
    {
        if (stat == "attack")
        {
            if (modifierType == "+")
            {
                _attackStatModifiersSum += value;
            }
            else if (modifierType == "*")
            {
                _attackStatModifiersMult += value / 100f - 1f;
            }
        }
        else if (stat == "defense")
        {
            if (modifierType == "+")
            {
                _defenseStatModifiersSum += value;
            }
            else if (modifierType == "*")
            {
                _defenseStatModifiersMult += value / 100f - 1f;
            }
        }

        if (duration > 0)
        {
            StartCoroutine(RemoveStatModifierAfterSomeTime(stat, modifierType, value, duration));
        }
    }

    private IEnumerator RemoveStatModifierAfterSomeTime(string stat, string modifierType, float value, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (stat == "attack")
        {
            if (modifierType == "+")
            {
                _attackStatModifiersSum -= value;
            }
            else if (modifierType == "*")
            {
                _attackStatModifiersMult -= value / 100f - 1f;
            }
        }
        else if (stat == "defense")
        {
            if (modifierType == "+")
            {
                _defenseStatModifiersSum -= value;
            }
            else if (modifierType == "*")
            {
                _defenseStatModifiersMult -= value / 100f - 1f;
            }
        }
    }

    public void RemoveStatModifier(string stat, string modifierType, float value, float duration = 0f)
    {
        if (stat == "attack")
        {
            if (modifierType == "+")
            {
                _attackStatModifiersSum -= value;
            }
            else if (modifierType == "*")
            {
                _attackStatModifiersMult -= value / 100f - 1f;
            }
        }
        else if (stat == "defense")
        {
            if (modifierType == "+")
            {
                _defenseStatModifiersSum -= value / 100f - 1f;
            }
            else if (modifierType == "*")
            {
                _defenseStatModifiersMult -= value / 100f - 1f;
            }
        }

        if (duration > 0)
        {
            StartCoroutine(AddStatModifierAfterSomeTime(stat, modifierType, value, duration));
        }
    }

    private IEnumerator AddStatModifierAfterSomeTime(string stat, string modifierType, float value, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (stat == "attack")
        {
            if (modifierType == "+")
            {
                _attackStatModifiersSum += value;
            }
            else if (modifierType == "*")
            {
                _attackStatModifiersMult += value / 100f - 1f;
            }
        }
        else if (stat == "defense")
        {
            if (modifierType == "+")
            {
                _defenseStatModifiersSum += value;
            }
            else if (modifierType == "*")
            {
                _defenseStatModifiersMult += value / 100f - 1f;
            }
        }
    }


    //Movement attributes
    //TODO refactor and move movement attributes out of state class
    private float movementSpeed = 11f;
    private float turnSpeed = 180f; //degrees/s

    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI attackUI;
    public TextMeshProUGUI defenseUI;

    private void Awake()
    {
        if (CurrentPlayer == null)
            CurrentPlayer = this;
    }

    void Start()
    {
        Debug.Log("Player start.");
        health = baseHealth/2;
    }
    
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed);
        transform.position += Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * movementSpeed;
        healthUI.text = (int)Health + " HP";
        attackUI.text = (int)Attack + " atk";
        defenseUI.text = (int)Defense + " def";
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0);
    }

    public void Heal (float amount)
    {
        health += amount;
        health = Mathf.Min(health, baseHealth);
    }
}
