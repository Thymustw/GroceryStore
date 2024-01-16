using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Data", menuName = "Character Stats/Unit Data")]
public class UnitData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public float baseMaxHealth;
    public int gunNumber;
    public float baseWalkSpeed;

    [Header("dsfds")]
    public float currentMaxHealth;
    public float currentHealth;
    public float currentWalkSpeed;
    
    void OnEnable()
    {
        currentMaxHealth = baseMaxHealth;
        currentHealth = currentMaxHealth;
        currentWalkSpeed = baseWalkSpeed;
    }
}
