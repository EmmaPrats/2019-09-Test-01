using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
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

    //Wander attributes
    //TODO these things should be editable in the inspector
    private float circleDistance = 1f;
    private float circleRadius = 0.5f;
    private float wanderAngle = 20f; //degrees
    private float displacementAngle = 0; //radians

    public WanderState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
    }

    public override Type Tick()
    {
        //Wander
        //TODO make it better
        displacementAngle += UnityEngine.Random.Range(-wanderAngle, wanderAngle); //TODO figure out which is better here: System.Random or UnityEngine.Random
        //transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * turnSpeed);
        transform.Rotate(new Vector3(0, 1, 0), displacementAngle * Time.deltaTime);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        /*
        Vector3 circlePosition = circleDistance * transform.forward;
        displacementAngle += UnityEngine.Random.Range(-wanderAngle, wanderAngle); //TODO figure out which is better here: System.Random or UnityEngine.Random
        Vector3 displacement = new Vector3(circleRadius * Mathf.Cos(displacementAngle),
                                           0,
                                           circleRadius * Mathf.Sin(displacementAngle));
        Vector3 desiredVelocity = Vector3.Normalize(circlePosition + displacement) * movementSpeed;
        Vector3 steer = desiredVelocity - transform.forward * movementSpeed;
        //TODO limit by maxForce
        //TODO refactor, use forces instead of manual Eulering
        Vector3 velocity = transform.forward * movementSpeed + steer * Time.deltaTime;
        transform.position += velocity;*/

        //TODO fix borders of the scene
        if (Vector3.Distance(new Vector3(), transform.position) > 30)
        {
            gameObject.SetActive(false);
        }

        //Check if player detected
        if (Vector3.Distance (transform.position, Player.CurrentPlayer.transform.position) <= detectionRange)
        {
            //Check if player in attack range
            if (Vector3.Distance (transform.position, Player.CurrentPlayer.transform.position) <= attackRange)
            {
                return typeof(AttackState);
            }
            else
            {
                return typeof(ChaseState);
            }
        }
        else
        {
            return null;
        }
    }
}
