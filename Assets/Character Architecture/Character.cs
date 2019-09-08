using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IMove, IHaveStats
{
    [SerializeField] private CharacterData characterData;

    #region MOVEMENT STATS

    private float movementSpeedModifiersMult = 1f;
    private float movementSpeedModifiersSum = 0f;

    public float MovementSpeed
    {
        get
        {
            return characterData.movementSpeed * movementSpeedModifiersMult + movementSpeedModifiersSum;
        }
    }

    private float turnSpeedModifiersMult = 1f;
    private float turnSpeedModifiersSum = 0f;

    public float TurnSpeed
    {
        get
        {
            return characterData.turnSpeed * turnSpeedModifiersMult + turnSpeedModifiersSum;
        }
    }

    #endregion

    #region STATS: HEALTH, ATTACK, DEFENSE

    private float healthModifiersMult = 1f;
    private float healthModifiersSum = 0f;

    public float MaxHealth
    {
        get
        {
            return characterData.health * healthModifiersMult + healthModifiersSum;
        }
    }

    private float healthLost;

    public float Health
    {
        get
        {
            return Mathf.Max(MaxHealth - healthLost, 0);
        }
        private set
        {
            healthLost = Mathf.Max(MaxHealth - value, 0);
        }
    }

    private float strengthModifiersMult = 1f;
    private float strengthModifiersSum = 0f;

    public float Strength
    {
        get
        {
            return characterData.strength * strengthModifiersMult + strengthModifiersSum;
        }
    }

    private float defenseModifiersMult = 1f;
    private float defenseModifiersSum = 0f;

    public float Defense
    {
        get
        {
            return characterData.defense * defenseModifiersMult + defenseModifiersSum;
        }
    }

    #endregion

    #region STATS MODIFICATIONS
    public void AddStatModifier(Stats stat, ModifierTypes modifierType, float value, float duration)
    {
        Debug.Log("AddStatModifier");
        switch (stat)
        {
            case Stats.health:
                if (modifierType == ModifierTypes.mult)
                    healthModifiersMult += value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    healthModifiersSum += value;
                break;
            case Stats.strength:
                if (modifierType == ModifierTypes.mult)
                    strengthModifiersMult += value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    strengthModifiersSum += value;
                break;
            case Stats.defense:
                if (modifierType == ModifierTypes.mult)
                    defenseModifiersMult += value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    defenseModifiersSum += value;
                break;
        }

        if (duration > 0)
        {
            StartCoroutine(RemoveStatModifierAfterSomeTime(stat, modifierType, value, duration));
        }
    }

    private IEnumerator RemoveStatModifierAfterSomeTime(Stats stat, ModifierTypes modifierType, float value, float duration)
    {
        yield return new WaitForSeconds(duration);

        switch (stat)
        {
            case Stats.health:
                if (modifierType == ModifierTypes.mult)
                    healthModifiersMult -= value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    healthModifiersSum -= value;
                break;
            case Stats.strength:
                if (modifierType == ModifierTypes.mult)
                    strengthModifiersMult -= value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    strengthModifiersSum -= value;
                break;
            case Stats.defense:
                if (modifierType == ModifierTypes.mult)
                    defenseModifiersMult -= value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    defenseModifiersSum -= value;
                break;
        }
    }

    public void RemoveStatModifier(Stats stat, ModifierTypes modifierType, float value, float duration)
    {
        switch (stat)
        {
            case Stats.health:
                if (modifierType == ModifierTypes.mult)
                    healthModifiersMult -= value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    healthModifiersSum -= value;
                break;
            case Stats.strength:
                if (modifierType == ModifierTypes.mult)
                    strengthModifiersMult -= value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    strengthModifiersSum -= value;
                break;
            case Stats.defense:
                if (modifierType == ModifierTypes.mult)
                    defenseModifiersMult -= value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    defenseModifiersSum -= value;
                break;
        }

        if (duration > 0)
        {
            StartCoroutine(AddStatModifierAfterSomeTime(stat, modifierType, value, duration));
        }
    }

    private IEnumerator AddStatModifierAfterSomeTime(Stats stat, ModifierTypes modifierType, float value, float duration)
    {
        yield return new WaitForSeconds(duration);

        switch (stat)
        {
            case Stats.health:
                if (modifierType == ModifierTypes.mult)
                    healthModifiersMult += value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    healthModifiersSum += value;
                break;
            case Stats.strength:
                if (modifierType == ModifierTypes.mult)
                    strengthModifiersMult += value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    strengthModifiersSum += value;
                break;
            case Stats.defense:
                if (modifierType == ModifierTypes.mult)
                    defenseModifiersMult += value / 100f - 1f;
                else if (modifierType == ModifierTypes.sum)
                    defenseModifiersSum += value;
                break;
        }
    }

    #endregion

    public float TakeDamage(float damage)
    {
        float damageTaken = Mathf.Min(Health, damage - Defense);
        Health -= damageTaken;
        return damageTaken;
    }

    public float Heal(float amount)
    {
        float amountHealed = Mathf.Min(MaxHealth - Health, amount);
        Health += amountHealed;
        return amountHealed;
    }

    //TODO modify, add setter, etc.
    public float AttackRange
    {
        get
        {
            return characterData.attackRange;
        }
    }

    //TODO modify, add setter, etc.
    public float AttackSpeed
    {
        get
        {
            return characterData.attackSpeed;
        }
    }
}
