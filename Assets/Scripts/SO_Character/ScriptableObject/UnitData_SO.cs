using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Data", menuName = "Character Stats/Unit Data")]
public class UnitData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public float maxHealth;
    public float currentHealth;
    public int gunNumber;
    public float walkSpeed;
    
    void OnEnable()
    {
        currentHealth = maxHealth;
    }
}
