using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ItemUIEvent : UnityEvent<ItemUI> {}


public class ItemUI : MonoBehaviour
{
    public Item item;
    public TextMeshProUGUI itemName;

    public ItemUIEvent onClicked;

    void Start()
    {
        if (item)
            Display(item);
    }

    public virtual void Display(Item item)
    {
        this.item = item;
        itemName.text = item.name;
    }

    public virtual void Click()
    {
        onClicked.Invoke(this);
    }
}
