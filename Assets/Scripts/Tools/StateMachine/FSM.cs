using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle, Patrol, Chase, Attack, React
}


[Serializable]
public class Parameter
{
    public int health;
    public Vector2 chasePos;

    public float idleTime;

    public float moveSpeed;
    public float chaseSpeed;

    public float patrolRadius;
    public float chaseRadius;

    //public Animator animator;
}


public class FSM : MonoBehaviour
{
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    public Parameter parameter = new Parameter();
    
    
    void Awake()
    {
        //parameter.animator = GetComponent<Animator>();
    }


    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.React, new ReactState(this));

        TransitionState(StateType.Idle);
    }


    void Update()
    {
        currentState.OnUpdate();
    }


    // Change the state.
    public void TransitionState(StateType type)
    {
        if(currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    public void FlipTo()
    {
        parameter.chasePos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (parameter.chasePos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }
}
