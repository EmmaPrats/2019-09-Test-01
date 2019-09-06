using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemMixin : MonoBehaviour
{
    public abstract void Tick(Player user);
}
