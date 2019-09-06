using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemEvent : UnityEvent<Item> { }

public class Item : MonoBehaviour
{
    //TODO make them protected and serialized?

    public IEnumerable type; //TODO come up with a system for this

    public bool consumable = true;

    public float useDuration = 0f;
    public float channelDuration = 0f;

    public bool rootsUserWhileUsing = false;
    public bool rootsUserWhileChanneling = false;

    protected ItemMixin[] mixins;
    protected List<ItemMixin> mixinsList;

    protected float channelCounter;
    protected float usingCounter;

    protected bool isInUse;

    protected bool isReady = false;

    protected void Start()
    {
        mixins = GetComponents<ItemMixin>();
        channelCounter = channelDuration;
        usingCounter = useDuration;
        isReady = true;
    }

    //Returns true if still using, false if not
    public bool Tick(Player user)
    {
        if (!isReady) //This is because Tick gets called before Start
            return true;

        if (channelCounter > 0) //is channeling
        {
            channelCounter -= Time.deltaTime;
            return true;
        }
        else //is using
        {
            mixins = GetComponents<ItemMixin>();
            for (int i=0; i<mixins.Length; i++)
            {
                mixins[i].Tick(user);
            }
            usingCounter -= Time.deltaTime;
        }

        if (usingCounter <= 0) //finished its use
            return false;
        else //still being used
            return true;
    }

    //Returns true if item is kept, false if destroyed
    public bool Use()
    {
        return !consumable;
    }
}
