using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamage
{
    public EnemyData data;

    private int currentHealth;

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = Math.Min(value, data.health);
        }
    }

    void Start()
    {
        //Debug.Log("Enemy start.");
        currentHealth = data.health;
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy taking damage = " + damage + ".");
        currentHealth -= (int)damage;
    }
}
