using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBoostMixin : ItemMixin
{
    public string stat;
    public string amount;

    public override void Tick(Player user)
    {
        Debug.Log("Boosting stat " + stat + " by " + amount + ".");
    }
}
