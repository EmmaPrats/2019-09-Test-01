using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBoostMixin : ItemMixin
{
    public string stat;
    public float amount;
    public bool isPercentage;

    private bool isActive = false;

    private Item item;

    private void Start()
    {
        item = GetComponent<Item>();
    }

    public override void Tick(Player user)
    {
        if (!isActive)
        {
            Debug.Log("Boosting stat " + stat + " by " + amount + (isPercentage ? " multiplicatively." : "."));
            user.AddStatModifier(stat, isPercentage ? "*" : "+", amount, item.useDuration);
            isActive = true;
        }
    }
}
