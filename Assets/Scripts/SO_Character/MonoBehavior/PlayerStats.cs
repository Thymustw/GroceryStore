using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public UnitData_SO unitData;
    public GunData_SO gunData;

    #region "Read from UnitData_SO"
    public float GetBaseMaxHealth()
    {
        if (unitData != null)
            return unitData.baseMaxHealth;
        else return 0;
    }

    public float GetCurrentMaxHealth()
    {
        if (unitData != null)
            return unitData.currentMaxHealth;
        else return 0;
    }

    public void SetCurrentMaxHealth(float value)
    {
        if (unitData != null)
            unitData.currentMaxHealth = value;
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

    public float GetCurrentRunSpeed()
    {
        if (unitData != null)
            return unitData.currentWalkSpeed;
        else return 0;
    }

    public void SetCurrentRunSpeed(float value)
    {
        if (unitData != null)
            unitData.currentWalkSpeed = value;
    }
    #endregion

    #region "Read from GunData_SO"
    public float GetCurrentDamage()
    {
        if (gunData != null)
            return gunData.currentDamage;
        else return 0;
    }

    public void SetCurrentDamage(float value)
    {
        if (gunData != null)
            gunData.currentDamage = value;
    }

    public float GetCurrentBulletSpeed()
    {
        if (gunData != null)
            return gunData.currentBulletSpeed;
        else return 0;
    }

    public void SetCurrentBulletSpeed(float value)
    {
        if (gunData != null)
            gunData.currentBulletSpeed = value;
    }


    public int GetCurrentBulletCount()
    {
        if(gunData != null)
            return gunData.currentBulletCount;
        else return 0;
    }

    public void SetCurrentBulletCount(int value)
    {
        if(gunData != null)
            gunData.currentBulletCount = value;
    }


    public int GetCurrentReboundTime()
    {
        if (gunData != null)
            return gunData.currentReboundTime;
        else return 0;
    }

    public void SetCurrentReboundTime(int value)
    {
        if (gunData != null)
            gunData.currentReboundTime = value;
    }


    public int GetCurrentNumPreShootBullet()
    {
        if (gunData != null)
            return gunData.currentNumPreShootBullet;
        else return 0;
    }

    public void SetCurrentNumPreShootBullet(int value)
    {
        if (gunData != null)
            gunData.currentNumPreShootBullet = value;
    }


    public float GetCurrentIntervalPreShoot()
    {
        if (gunData != null)
            return gunData.currentIntervalPreShoot;
        else return 0;
    }
    public void SetCurrentIntervalPreShoot(float value)
    {
        if (gunData != null)
            gunData.currentIntervalPreShoot = value;
    }


    public float GetCurrentIntervalPreBullet()
    {
        if (gunData != null)
            return gunData.currentIntervalPreBullet;
        else return 0;
    }

    public void SetCurrentIntervalPreBullet(float value)
    {
        if (gunData != null)
            gunData.currentIntervalPreBullet = value;
    }
    #endregion
}
