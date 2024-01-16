using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Character Stats/Enemy Data")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Stats Info")]
    public int maxHealth;
    public int currentHealth;
    //public int damage;
    public LayerMask targetLayer = 1 << 7;

    // Idle State
    public float idleTime;

    // Patrol State
    public float patrolSpeed;
    public float patrolRadius;

    // Chase State
    public float chaseSpeed;
    public float chaseRadius;

    // Attack State
    public float attackRadius;
    public float attackTime;
}
