using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingMixin : ItemMixin
{
    [SerializeField] private float healAmount;
    private float healPerSecond;

    private Item item;

    private void Start()
    {
        item = GetComponent<Item>();

        if (item.useDuration > 0)
        {
            healPerSecond = healAmount / item.useDuration;
        }
    }

    public override void Tick(Character user)
    {
        if (item.useDuration > 0)
        {
            //.Log("Healing " + healPerSecond * Time.deltaTime + " damage over time for a total of " + healAmount + " damage healed.");
            user.Heal(healPerSecond * Time.deltaTime);
        }
        else
        {
            //Debug.Log("Healing " + healAmount + " damage.");
            user.Heal(healAmount);
        }
        //Debug.Log(Player.CurrentPlayer.Health);
    }
}
