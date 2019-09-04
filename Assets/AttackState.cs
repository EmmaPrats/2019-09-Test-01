using System;
using UnityEngine;

public class AttackState : BaseState
{
    public Enemy enemy;

    //Movement attributes
    //TODO refactor and move movement attributes out of state class
    private float movementSpeed = 10f;
    private float turnSpeed = 180f; //degrees/s

    //Player detection and attacking attributes
    //TODO refactor and move player detection and attacking attributes
    private float detectionRange = 5f;
    //TODO this is a radius now, refactor and make it a cone in front of the enemy
    private float attackRange = 1.5f;

    //TODO refactor attacks separately
    private float attackCooldown = 1f;
    private float attackTimer = 1f;

    public AttackState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }

    public override Type Tick()
    {
        //TODO attack
        if (attackTimer < 0)
        {
            Debug.Log("Attacking.");
            attackTimer = attackCooldown;
        }
        else
        {
            transform.LookAt(Player.CurrentPlayer.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        attackTimer -= Time.deltaTime;


        //Check if player still in range
        if (Vector3.Distance(transform.position, Player.CurrentPlayer.transform.position) <= attackRange)
        {
            return null;
        }
        else if (Vector3.Distance(transform.position, Player.CurrentPlayer.transform.position) <= detectionRange)
        {
            return typeof(ChaseState);
        }
        else
        {
            return typeof(WanderState);
        }
    }
}