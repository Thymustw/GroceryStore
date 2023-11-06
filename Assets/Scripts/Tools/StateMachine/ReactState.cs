using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactState : IState
{
    private FSM manager;
    private Parameter parameter;
    
    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {

    }


    public void OnUpdate()
    {

    }


    public void OnExit()
    {

    }
}
