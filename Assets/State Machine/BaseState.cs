using System;
using UnityEngine;

public abstract class BaseState
{
    protected GameObject gameObject; //Cached them so we can access them fast
    protected Transform transform; //Cached them so we can access them fast

    public abstract Type Tick();

    public BaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }
}
