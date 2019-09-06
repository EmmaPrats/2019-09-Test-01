using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Vector3 attackPos;
    [SerializeField] private LayerMask whatIsEnemies;
    [SerializeField] private float attackRange;

    [SerializeField] private int damage;


    [SerializeField] private float attackCooldown = 0f;
    private float attackCounter;

    private void Start()
    {
        attackCounter = attackCooldown;
    }

    void Update()
    {
        if (attackCounter <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Collider[] enemiesToDamage = Physics.OverlapSphere(transform.position+attackPos, attackRange);
                for (int i=0; i<enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>()?.TakeDamage(damage);
                }

                attackCounter = attackCooldown;
            }
        }
        else
            attackCounter -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position+attackPos, attackRange);
    }
}
