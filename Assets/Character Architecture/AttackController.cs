using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Character character; //[SerializeField] private IHaveStats character;

    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask layerMask;

    private float attackCounter = 0f;

    void Update()
    {
        if (attackCounter <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Collider[] enemiesToDamage = Physics.OverlapSphere(attackPosition.position, character.AttackRange, layerMask);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    float? damageDealt = enemiesToDamage[i].GetComponent<Character>()?.TakeDamage(character.Strength);
                }

                attackCounter = character.AttackSpeed;
            }
        }
        else
            attackCounter -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, character.AttackRange);
    }
}
