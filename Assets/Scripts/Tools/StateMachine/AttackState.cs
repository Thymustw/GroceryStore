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
        AnimatorStateInfo stateinfo = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if(stateinfo.normalizedTime >= 1f)
        {
            GameManager.Instance.DamageCount(parameter.target.gameObject, parameter.enemyStats.GetDamage());
            manager.TransitionState(StateType.Idle);
        }
    }


    public void OnExit()
    {

    }
}
