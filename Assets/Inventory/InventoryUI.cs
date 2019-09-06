using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform content;
    public ItemUI itemUIPrefab;

    public bool showPlayerInventory = false;

    public ItemEvent itemSelected;

    void Start()
    {
        if (showPlayerInventory)
            Display(Inventory.playerInventory);
        else if (inventory)
            Display(inventory);
    }

    public virtual void Display (Inventory i)
    {
        if (this.inventory)
            this.inventory.onChanged.RemoveListener(Refresh);

        this.inventory = i;
        this.inventory.onChanged.AddListener(Refresh);
        Refresh();
    }

    public virtual void Refresh()
    {
        foreach (Transform t in content)
        {
            Destroy(t.gameObject);
        }

        foreach (Item i in inventory.itemPrefabs)
        {
            ItemUI ui = ItemUI.Instantiate(itemUIPrefab, content);
            ui.onClicked.AddListener(UIClicked);
            ui.Display(i);
        }
    }

    public virtual void UIClicked(ItemUI iui)
    {
        itemSelected.Invoke(iui.item);

        //Inventory.playerInventory.Add(iui.item);
        //inventory.Remove(iui.item);
        //Destroy(iui.gameObject);
    }

    // V   Code referenced by UnityEvents only   V

    #region UnityEventResponders

    public void AddToPlayerInventory(Item item)
    {
        Inventory.playerInventory.Add(item);
    }

    public void RemoveFromOwnInventory (Item item)
    {
        inventory.Remove(item);
    }

    public void Use(Item item)
    {
        //TODO this doesn't check if I'm already using one of this type.
        inventory.itemUsed.Invoke(item);



        //Item i = Item.Instantiate(item, content);

        if (!item.Use())
        {
            RemoveFromOwnInventory(item);
        }
    }

    #endregion
}
