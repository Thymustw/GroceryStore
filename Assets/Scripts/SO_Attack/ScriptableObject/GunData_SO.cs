using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Attack Data")]
public class GunData_SO : ScriptableObject
{
    [Header("Bullet Info")]
    public float baseDamage;
    public float baseBulletSpeed;

    [Header("ShotGun Info")]
    public int baseBulletCount;
    public int baseReboundTime;
    public int baseNumPreShootBullet;
    public float baseIntervalPreShoot;
    public float baseIntervalPreBullet;

    [Header("CURRENT ShotGun Info(DONT CHANGE)")]
    public float currentDamage;
    public float currentBulletSpeed;

    public int currentBulletCount;
    public int currentReboundTime;
    public int currentNumPreShootBullet;
    public float currentIntervalPreShoot;
    public float currentIntervalPreBullet;

    void OnEnable() 
    {
        currentDamage = baseDamage;
        currentBulletSpeed = baseBulletSpeed;

        currentBulletCount = baseBulletCount;
        currentReboundTime = baseReboundTime;
        currentNumPreShootBullet = baseNumPreShootBullet;
        currentIntervalPreShoot = baseIntervalPreShoot;
        currentIntervalPreBullet = baseIntervalPreBullet;
    }
}