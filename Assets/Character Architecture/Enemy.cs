using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IHaveAI
{
    private Transform target;

    public Transform Target
    {
        get => target;
        set
        {
            target = value;
        }
    }

    void Update()
    {
        if (Health <= 0)
            Destroy(gameObject);
    }
}