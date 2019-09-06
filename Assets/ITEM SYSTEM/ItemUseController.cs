using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseController : MonoBehaviour
{
    public Player character;
    public Inventory inventory;

    [SerializeField] private List<Item> itemsInUse;

    //This is for testing
    public static ItemUseController playerItemUseController;

    private void Awake()
    {
        playerItemUseController = this;
    }

    private void Start()
    {
        itemsInUse = new List<Item>();

        character = Player.CurrentPlayer;
        inventory = Inventory.playerInventory;
        inventory.itemUsed.AddListener(UseItem);
    }

    //For some reason, if I instantiate here it won't have the Item component
    private void UseItem(Item item)
    {
        //TODO check if I'm already using one of this type

        itemsInUse.Add(Instantiate(item, transform));
    }

    void Update()
    {
        for (int i=itemsInUse.Count-1; i>=0; i--)
        {
            if (!itemsInUse[i].Tick(character))
            {
                Destroy(itemsInUse[i].gameObject); //Destroy gives me trouble, but if I don't I will keep accumulating garbage
                itemsInUse.RemoveAt(i);
            }
        }
    }
}
