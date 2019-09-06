using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour //Doesn't have to be a MonoBehaviour, but in this case it made sense. So that the state machine's Update is what is controlling the game object.
{
    private Dictionary<Type, BaseState> availableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChanged;

    public void SetStates (Dictionary<Type, BaseState> states)
    {
        availableStates = states;
    }

    //This was in the Enemy class in the example
    private void Awake()
    {
        InitializeStateMachine();
    }

    //This was in the Enemy class in the example
    //Idea: We load data from a JSON, we parse through all the IEnums, and we add only the ones we want for this agent.
    private void InitializeStateMachine() //We could replace this for a factory
    {
        var states = new Dictionary<Type, BaseState>() //TODO this should be doable with the magic from the other example IEnumeration
        {
            { typeof (WanderState), new WanderState (gameObject.GetComponent<Enemy>()) },
            { typeof (ChaseState), new ChaseState (gameObject.GetComponent<Enemy>()) },
            { typeof (AttackState), new AttackState (gameObject.GetComponent<Enemy>()) }
        };

        SetStates(states);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == null)
            CurrentState = availableStates.Values.First();

        var nextState = CurrentState?.Tick();

        if (nextState != null && nextState != CurrentState?.GetType())
            SwitchToNewState(nextState);
    }

    private void SwitchToNewState (Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
