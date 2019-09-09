using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EnemyEvent : UnityEvent<Enemy> { }

public class Enemy : Character, IHaveAI
{
    private Transform target;

    public EnemyEvent onEnemyDie;

    void Start()
    {
        GameController.instance.ListenToThis(this);
    }

    public Transform Target
    {
        get => target;
        set
        {
            target = value;
        }
    }

    void Update()
    {
        if (Health <= 0)
        {
            onEnemyDie.Invoke(this);
            Destroy(gameObject);
        }
    }
}