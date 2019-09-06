using System;
using UnityEngine;

public class ChaseState : BaseState
{
    public Enemy enemy;

    public ChaseState (Enemy enemy) : base (enemy.gameObject)
    {
        this.enemy = enemy;
    }

    public override Type Tick()
    {
        //Chase
        transform.LookAt(Player.CurrentPlayer.transform); //TODO 
        transform.Translate(Vector3.forward * Time.deltaTime * enemy.data.movementSpeed);

        //Check if player detected
        if (Vector3.Distance(transform.position, Player.CurrentPlayer.transform.position) <= enemy.data.detectionRange)
        {
            //Check if player in attack range
            if (Vector3.Distance(transform.position, Player.CurrentPlayer.transform.position) <= enemy.data.attackRange)
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