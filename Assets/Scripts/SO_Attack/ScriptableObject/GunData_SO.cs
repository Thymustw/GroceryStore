using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Attack Data")]
public class GunData_SO : ScriptableObject
{
    [Header("Bullet Info")]
    public float damage;
    public float bulletSpeed;

    [Header("ShotGun Info")]
    public int maxBulletCount;
    public int maxReboundTime;
    public int numPreShootBullet;
    public float intervalPreShoot;
    public float intervalPreBullet;
}