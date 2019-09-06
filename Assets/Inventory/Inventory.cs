using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory playerInventory;

    public List<Item> items;
    public List<Item> itemPrefabs;

    public bool isPlayerInventory = false;

    public UnityEvent onChanged;
    public ItemEvent itemUsed;

    private void Awake()
    {
        if (isPlayerInventory)
            playerInventory = this;
    }

    public void Add(Item item)
    {
        itemPrefabs.Add(item);
        onChanged.Invoke();
    }

    public void Remove(Item item)
    {
        if (!itemPrefabs.Contains(item))
            return;

        itemPrefabs.Remove(item);
        onChanged.Invoke();
    }
}
