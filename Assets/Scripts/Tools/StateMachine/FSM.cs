using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle, Patrol, Chase, Attack, React
}


[Serializable]
public class Parameter
{
    //public EnemyScriptableObject enemy;

    public EnemyStats enemyStats;

    public Transform target;
    public float currentHealth;
    /*public LayerMask targetLayer
    {
        get{ return enemy.targetLayer;}
    }

    // Idle
    public float idleTime
    {
        get{ return enemy.idleTime;}
    }

    // Patrol
    public float patrolRadius
    {
        get{ return enemy.patrolRadius;}
    }
    public float patrolSpeed
    {
        get{ return enemy.patrolSpeed;}
    }

    // Chase
    public float chaseSpeed
    {
        get{ return enemy.chaseSpeed;}
    }
    public float chaseRadius
    {
        get{ return enemy.chaseRadius;}
    }*/

    // Attack
    public Transform attackPoint;
    /*public float attackRadius
    {
        get{ return enemy.attackRadius;}
    }*/

    public Animator animator;
    public GameObject bulletPrefab;
    public bool inBattle;
}


public class FSM : MonoBehaviour, IEndGameObserver
{
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    public Parameter parameter = new Parameter();
    //private bool isDead = false;
    
    
    void Awake()
    {
        parameter.animator = GetComponent<Animator>();
        parameter.enemyStats = GetComponent<EnemyStats>();
        parameter.attackPoint = transform.GetChild(0);
        parameter.target = GameObject.FindGameObjectWithTag("Player").transform;

        parameter.currentHealth = parameter.enemyStats.GetMaxHealth();
    }

    void OnEnable()
    {
        GameManager.Instance.AddEndGameObserver(this);
    }


    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.React, new ReactState(this));

        TransitionState(StateType.Idle);
        GameManager.Instance.AddWaitGameObjectAndSetActiveFalse(this.gameObject);
        parameter.inBattle = true;
    }


    void Update()
    {
        if (parameter.currentHealth == 0)
            //isDead = true;
            Destroy(this.gameObject);

        currentState.OnUpdate();
        //print(parameter.enemyStats.GetCurrentHealth());
    }

    void OnDisable()
    {
        GameManager.Instance.RemoveEndGameObserver(this);
        if(GameManager.Instance.GetEnemyDeadAndShootTenCrossBullet() && parameter.inBattle)
        {
            GameObject tempL = ObjectPool.Instance.GetObject(parameter.bulletPrefab);
            tempL.transform.position = this.transform.position;
            tempL.transform.localScale = new Vector3 (0.75f, 0.75f);
            tempL.GetComponent<Bullet>().SetSpeed(Vector2.left);

            GameObject tempR = ObjectPool.Instance.GetObject(parameter.bulletPrefab);
            tempR.transform.position = this.transform.position;
            tempR.transform.localScale = new Vector3 (0.75f, 0.75f);
            tempR.GetComponent<Bullet>().SetSpeed(Vector2.right);

            GameObject tempU = ObjectPool.Instance.GetObject(parameter.bulletPrefab);
            tempU.transform.position = this.transform.position;
            tempU.transform.localScale = new Vector3 (0.75f, 0.75f);
            tempU.GetComponent<Bullet>().SetSpeed(Vector2.up);

            GameObject tempD = ObjectPool.Instance.GetObject(parameter.bulletPrefab);
            tempD.transform.position = this.transform.position;
            tempD.transform.localScale = new Vector3 (0.75f, 0.75f);
            tempD.GetComponent<Bullet>().SetSpeed(Vector2.down);
        }

        if (GameManager.Instance.GetEnemyDeadAndShootCrossBullet() && parameter.inBattle)
        {
            GameObject tempL = ObjectPool.Instance.GetObject(parameter.bulletPrefab);
            tempL.transform.position = this.transform.position;
            tempL.transform.localScale = new Vector3 (0.75f, 0.75f);
            Vector2 tempVec2L = new Vector2(1,1);
            tempL.GetComponent<Bullet>().SetSpeed(tempVec2L.normalized);

            GameObject tempR = ObjectPool.Instance.GetObject(parameter.bulletPrefab);
            tempR.transform.position = this.transform.position;
            tempR.transform.localScale = new Vector3 (0.75f, 0.75f);
            Vector2 tempVec2R = new Vector2(-1,1);
            tempR.GetComponent<Bullet>().SetSpeed(tempVec2R.normalized);

            GameObject tempU = ObjectPool.Instance.GetObject(parameter.bulletPrefab);
            tempU.transform.position = this.transform.position;
            tempU.transform.localScale = new Vector3 (0.75f, 0.75f);
            Vector2 tempVec2U = new Vector2(-1,-1);
            tempU.GetComponent<Bullet>().SetSpeed(tempVec2U.normalized);

            GameObject tempD = ObjectPool.Instance.GetObject(parameter.bulletPrefab);
            tempD.transform.position = this.transform.position;
            tempD.transform.localScale = new Vector3 (0.75f, 0.75f);
            Vector2 tempVec2D = new Vector2(1,-1);
            tempD.GetComponent<Bullet>().SetSpeed(tempVec2D.normalized);
        }
    }


    // Change the state.
    public void TransitionState(StateType type)
    {
        if(currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }


    // Face to target.
    public void FlipTo(Transform target)
    {
        if (target != null)
        {
            if (target.position.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }
    }

    public void EndNotify()
    {
        parameter.animator.StopPlayback();
        parameter.target = null;
    }

    // Check player in chaseradius.
    /*public bool PlayerInChaseRange()
    {
        if (Physics2D.OverlapCircle(transform.position, parameter.chaseRadius, parameter.targetLayer))
        {
            parameter.target = GameObject.FindGameObjectWithTag("Player").transform;
            return true;
        }
        else
        {
            parameter.target = null;
            return false;
        }
    }*/
    

    // Debug drawing.
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        if (parameter.target)
            Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.enemyStats.GetAttackRadius());
    }
}
