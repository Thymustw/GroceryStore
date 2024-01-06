using UnityEngine;

public class ItemStats : MonoBehaviour
{
    public ItemData_SO itemData;

    #region "Read from ItemkData_SO"
    public int GetPlusBulletCount()
    {
        if (itemData != null)
            return itemData.plusBulletCount;
        else return 0;
    }

    public int GetPlusReboundTime()
    {
        if (itemData != null)
            return itemData.plusReboundTime;
        else return 0;
    }

    public int GetPlusNumPreShootBullet()
    {
        if (itemData != null)
            return itemData.plusNumPreShootBullet;
        else return 0;
    }

    public float GetMinusIntervalPreShoot()
    {
        if (itemData != null)
            return itemData.minusIntervalPreShoot;
        else return 0;
    }

    public float GetMinusIntervalPreBullet()
    {
        if (itemData != null)
            return itemData.minusIntervalPreBullet;
        else return 0;
    }


    public float GetPlusBulletSpeed()
    {
        if (itemData != null)
            return itemData.plusBulletSpeed;
        else return 0;
    }

    public float GetTimesBulletSize()
    {
        if (itemData != null)
            return itemData.timesBulletSize;
        else return 0;
    }


    public float GetTimesDamagePersentage()
    {
        if (itemData != null)
            return itemData.timesDamagePersentage;
        else return 0;
    }

    public float GetPlusBulletDamage()
    {
        if (itemData != null)
            return itemData.plusBulletDamage;
        else return 0;
    }

    public float GetTimesRunSpeed()
    {
        if (itemData != null)
            return itemData.timesRunSpeed;
        else return 0;
    }

    public float GetPlusHealth()
    {
        if (itemData != null)
            return itemData.plusHealth;
        else return 0;
    }
    #endregion
}
