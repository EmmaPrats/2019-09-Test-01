using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    float TakeDamage(float damage);
    float Heal(float amount);
}
