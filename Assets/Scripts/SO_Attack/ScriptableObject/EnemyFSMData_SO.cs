using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack Data", menuName = "Attack/Enemy Attack Data")]
public class EnemyFSMData_SO : ScriptableObject
{
    [Header("Status")]
    public float damage;
    public LayerMask targetLayerMask;

    [Header("Idle")]
    public float idleTime;

    /*[Header("Patrol")]
    public float patrolSpeed;
    public float patrolRadius;*/
    
    [Header("Chase")]
    public float chaseSpeed;
    //public float chaseRadius;

    [Header("Attack")]
    //public Transform attackPoint;
    public float attackRadius;
}
