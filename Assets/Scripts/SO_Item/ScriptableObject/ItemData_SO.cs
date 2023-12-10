using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item/Item Data")]
public class ItemData_SO : ScriptableObject
{
    [Header("ShotGun Info")]
    public int plusBulletCount;
    public int plusReboundTime;
    public int plusNumPreShootBullet;
    public float minusIntervalPreShoot;
    public float minusIntervalPreBullet;

    [Header("Bullet Info")]
    public float plusBulletSpeed;
    public float timesBulletSize;

    [Header("Value Info")]
    public float timesDamagePersentage;
    public float plusBulletDamage;
}
