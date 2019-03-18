using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkModel : HawkBase
{
    public HawkBase swoopState;
    public HawkBase patrolState;

    public HawkBase currentState;

    public void ChangeState(HawkBase newState)
    {
        newState.Enter();
        currentState = newState;
    }

    private void Awake()
    {
        // On awake, activate patrol state (for now)
        ChangeState(patrolState);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }
}
