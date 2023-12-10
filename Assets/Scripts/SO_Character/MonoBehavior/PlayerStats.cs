using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public UnitData_SO unitData;
    public GunData_SO gunData;

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

    #region "Read from GunData_SO"
    public float GetDamage()
    {
        if (gunData != null)
            return gunData.damage;
        else return 0;
    }

    public float GetBulletSpeed()
    {
        if (gunData != null)
            return gunData.bulletSpeed;
        else return 0;
    }


    public int GetMaxBulletCount()
    {
        if(gunData != null)
            return gunData.maxBulletCount;
        else return 0;
    }

    public void SetMaxBulletCount(int value)
    {
        if(gunData != null)
            gunData.maxBulletCount = value;
    }


    public int GetMaxReboundTime()
    {
        if (gunData != null)
            return gunData.maxReboundTime;
        else return 0;
    }

    public void SetMaxReboundTime(int value)
    {
        if (gunData != null)
            gunData.maxReboundTime = value;
    }


    public int GetNumPreShootBullet()
    {
        if (gunData != null)
            return gunData.numPreShootBullet;
        else return 0;
    }

    public void SetNumPreShootBullet(int value)
    {
        if (gunData != null)
            gunData.numPreShootBullet = value;
    }


    public float GetIntervalPreShoot()
    {
        if (gunData != null)
            return gunData.intervalPreShoot;
        else return 0;
    }
    public void SetIntervalPreShoot(float value)
    {
        if (gunData != null)
            gunData.intervalPreShoot = value;
    }


    public float GetIntervalPreBullet()
    {
        if (gunData != null)
            return gunData.intervalPreBullet;
        else return 0;
    }

    public void SetIntervalPreBullet(float value)
    {
        if (gunData != null)
            gunData.intervalPreBullet = value;
    }
    #endregion
}
