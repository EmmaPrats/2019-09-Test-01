using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Stats
{
    health,
    strength,
    defense
}

public enum ModifierTypes
{
    mult,
    sum
}

public interface IHaveStats
{
    float MaxHealth { get; }
    float Health { get; }
    float Strength { get; }
    float Defense { get; }

    void AddStatModifier(Stats stat, ModifierTypes modifierType, float value, float duration = 0f);
    void RemoveStatModifier(Stats stat, ModifierTypes modifierType, float value, float duration = 0f);

    float TakeDamage(float damage);
    float Heal(float amount);
}
