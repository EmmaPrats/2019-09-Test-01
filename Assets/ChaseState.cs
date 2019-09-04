using System;
using UnityEngine;

public class ChaseState : BaseState
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

    public ChaseState (Enemy enemy) : base (enemy.gameObject)
    {
        this.enemy = enemy;
    }

    public override Type Tick()
    {
        //Chase
        transform.LookAt(Player.CurrentPlayer.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

        //Check if player detected
        if (Vector3.Distance(transform.position, Player.CurrentPlayer.transform.position) <= detectionRange)
        {
            //Check if player in attack range
            if (Vector3.Distance(transform.position, Player.CurrentPlayer.transform.position) <= attackRange)
            {
                return typeof(AttackState);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return typeof(WanderState);
        }
    }
}