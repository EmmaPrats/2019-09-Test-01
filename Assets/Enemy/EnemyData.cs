using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public string name;

    public int health;

    //Movement attributes
    public float movementSpeed = 10f;
    public float turnSpeed = 180f; //degrees/s

    //Player detection and attacking attributes
    public float detectionRange = 5f; //TODO detectionRange is a radius now, refactor and make it a cone in front of the enemy
    public float attackRange = 1.5f; //TODO attackRange is a radius now, refactor and make it a cone in front of the enemy

    public Color color;

    public float attackDamage = 10f;

    public void myMethod()
    {
        int i = 1;

        int b = i + 2;
    }

    public GameObject playerPrefab;

    public ItemMixin mixin;
}
