using System;
using UnityEngine;

public class AttackState : BaseState
{
    public Enemy enemy;

    //TODO refactor attacks separately
    private float attackCooldown = 1f;
    private float attackTimer = 0;

    public AttackState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }

    public override Type Tick()
    {
        //TODO attack
        if (attackTimer < 0)
        {
            Player.CurrentPlayer.TakeDamage(enemy.data.attackDamage);
            attackTimer = attackCooldown;
        }
        else
        {
            transform.LookAt(Player.CurrentPlayer.transform);
            transform.Translate(Vector3.forward * Time.deltaTime * enemy.data.movementSpeed);
        }
        attackTimer -= Time.deltaTime;


        //Check if player still in range
        if (Vector3.Distance(transform.position, Player.CurrentPlayer.transform.position) <= enemy.data.attackRange)
        {
            return null;
        }
        else if (Vector3.Distance(transform.position, Player.CurrentPlayer.transform.position) <= enemy.data.detectionRange)
        {
            return typeof(ChaseState);
        }
        else
        {
            return typeof(WanderState);
        }
    }
}