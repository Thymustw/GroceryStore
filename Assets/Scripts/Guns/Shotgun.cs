using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    // Shotguns parameter.
    private int bulletCount;
    private int numPreShoot;
    public float bulletAngle = 15;


    protected override void Fire()
    {
        // Play anime.
        animator.SetTrigger("Shoot");

        // Get the gun status before fire.
        bulletCount = GameManager.Instance.maxBulletCount;
        numPreShoot = GameManager.Instance.numPreShoot;

        // Find center to calculate the direction of each bullet.
        int median = bulletCount / 2;
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
            bullet.transform.position = muzzlePos.position;
            
            // Even or odd, then count and shoot.
            if(bulletCount % 2 == 1)
            {
                bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median), Vector3.forward) * direction);
            }
            else
            {
                bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median) + bulletAngle / 2, Vector3.forward) * direction);
            }
        }

        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }

    //TODO:Create multi time shoot func. Use IEnumerator
}
