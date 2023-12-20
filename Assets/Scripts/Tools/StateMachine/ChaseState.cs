using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;
    
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Run");
    }


    public void OnUpdate()
    {
        // Check player in chase range.
        //manager.PlayerInChaseRange();

        // Face to target.
        manager.FlipTo(parameter.target);


        // If in chase range, then chase.
        if (parameter.target)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position, parameter.target.position, parameter.enemyStats.GetChaseSpeed() * Time.deltaTime);
        }
        // If lose the target then change state to IDLE.
        //if (parameter.target == null)
        //{
        //    manager.TransitionState(StateType.Idle);
        //}


        // if in attack range, then change state to ATTACK.
        if (Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.enemyStats.GetAttackRadius(), parameter.enemyStats.GetTargetLayerMask()) && parameter.target)
        {
            manager.TransitionState(StateType.Attack);
        }
    }


    public void OnExit()
    {

    }
}
