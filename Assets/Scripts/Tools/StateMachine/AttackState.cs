using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;
    
    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Attack");
    }


    public void OnUpdate()
    {
        GameManager.Instance.DamageCount(parameter.target.gameObject, parameter.enemyStats.GetDamage());
        manager.TransitionState(StateType.Idle);
    }


    public void OnExit()
    {

    }
}
