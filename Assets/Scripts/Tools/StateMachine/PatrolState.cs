public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;
    
    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        //parameter.animator.Play("Run");
    }


    public void OnUpdate()
    {

    }


    public void OnExit()
    {

    }
}
