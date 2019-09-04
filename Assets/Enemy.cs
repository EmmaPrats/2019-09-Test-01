using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;

    void Start()
    {
        //Debug.Log("Enemy start.");
        player = Player.CurrentPlayer;
    }

    void Update()
    {

    }
}
