using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public UnitData_SO unitData;
    public EnemyFSMData_SO enemyFSMData_SO;

    #region "Read from UnitData_SO"
    public float GetMaxHealth()
    {
        if (unitData != null)
            return unitData.maxHealth;
        else return 0;
    }


    public float GetCurrentHealth()
    {

        if (unitData != null)
            return unitData.currentHealth;
        else return 0;
    }

    public void SetCurrentHealth(float value)
    {
        if (unitData != null)
            unitData.currentHealth = value;
    }


    public int GetGunNumber()
    {
        if (unitData != null && this.gameObject.tag == "Player")
            return unitData.gunNumber;
        else return 0;
    }

    public void SetGunNumber(int value)
    {
        if (unitData != null)
            unitData.gunNumber = value;
    }

    public float GetWalkSpeed()
    {
        if (unitData != null)
            return unitData.walkSpeed;
        else return 0;
    }

    public void SetWalkSpeed(float value)
    {
        if (unitData != null)
            unitData.walkSpeed = value;
    }
    #endregion
    
    #region  "Read from EnemyFSMData_SO"
    public float GetDamage()
    {
        if (enemyFSMData_SO != null)
            return enemyFSMData_SO.damage;
        else return 0;
    }

    public LayerMask GetTargetLayerMask()
    {
        if (enemyFSMData_SO != null)
            return enemyFSMData_SO.targetLayerMask;
        else return 0;
    }

    /*public Transform GetTarget()
    {
        if (enemyFSMData_SO != null)
            return enemyFSMData_SO.target;
        else return null;
    }*/


    public float GetIdleTime()
    {
        if (enemyFSMData_SO != null)
            return enemyFSMData_SO.idleTime;
        else return 0;
    }


    public float GetChaseSpeed()
    {
        if (enemyFSMData_SO != null)
            return enemyFSMData_SO.chaseSpeed;
        else return 0;
    }


    public float GetAttackRadius()
    {
        if (enemyFSMData_SO != null)
            return enemyFSMData_SO.attackRadius;
        else return 0;
    }
    #endregion
}
