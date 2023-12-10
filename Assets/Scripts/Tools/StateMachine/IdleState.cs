using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;
    private float timer;
    
    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Idle");
    }


    public void OnUpdate()
    {
        // Wait for idle time, then change state to CHASE.
        timer += Time.deltaTime;
        if (timer >= parameter.enemyStats.GetIdleTime())
        {
            //if(manager.PlayerInChaseRange())
                manager.TransitionState(StateType.Chase);
            //else
            //    manager.TransitionState(StateType.Patrol);
        }
    }


    public void OnExit()
    {
        timer = 0;
    }
}
