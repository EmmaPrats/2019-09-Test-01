using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    [Header("Narrative Settings")]
    new public string name;

    [Header("Movement Settings")]
    [Tooltip("meters per second")]
    public float movementSpeed;
    [Tooltip("degrees per second")]
    public float turnSpeed;

    [Header("Stats Settings")]
    public int health = 100;
    public int strength = 10;
    public int defense = 10;
    [Tooltip("meters")]
    public float attackRange = 1f;
    [Tooltip("attacks per second")]
    public float attackSpeed = 1f;
}
