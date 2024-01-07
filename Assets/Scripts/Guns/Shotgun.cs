using System.Collections;
using UnityEngine;

public class Shotgun : Gun
{
    // Shotguns parameter.
    private int maxBulletCount;
    private int numPreShootBullet;
    private float intervalPreBullet;
    //private Transform firstShootPos;
    public float bulletAngle = 15;

    protected override void Awake() 
    {
        base.Awake();
    }


    protected override void Fire()
    {
        // Get the gun status before fire.
        maxBulletCount = GameManager.Instance.MaxBulletCount();
        numPreShootBullet = GameManager.Instance.MaxNumPreShootBullet();
        intervalPreBullet = GameManager.Instance.IntervalPreBullet();

        // TODO:Get the first shoot position, and shoot.
        //firstShootPos = GameObject.Find("Muzzle").transform;
        StartCoroutine(ShootManyTimes(numPreShootBullet, intervalPreBullet));
    }


    // Find center to calculate the direction of each bullet.
    private void ShotGunShoot()
    {
        int median = maxBulletCount / 2;
        for (int i = 0; i < maxBulletCount; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
            bullet.transform.position = muzzlePos.position;
            
            float randomAngle = Random.Range(-2f, 2f);
            // Even or odd, then count and shoot.
            if(maxBulletCount % 2 == 1)
            {
                bullet.transform.localScale = new Vector3 (0.75f, 0.75f);
                bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median) + randomAngle, Vector3.forward) * direction);
            }
            else
            {
                bullet.transform.localScale = new Vector3 (0.75f, 0.75f);
                bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median) + bulletAngle / 2 + randomAngle, Vector3.forward) * direction);
            }
        }

        //GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        //shell.transform.position = shellPos.position;
        //shell.transform.rotation = shellPos.rotation;
    }


    // Multi time shoot.
    // TODO: Turn back the final time to avoid the shoot interval.
    private IEnumerator ShootManyTimes(int preShootInterval, float preBulletInterval)
    {      
        for (int i = 0; i < preShootInterval; i++)
        {
            // Play anime.
            animator.SetTrigger("Shoot");
            AudioManager.Instance.PlayAudio(new IPlayAudioShootSound());

            ShotGunShoot();
            yield return new WaitForSeconds(preBulletInterval);
        }
    } 

    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
