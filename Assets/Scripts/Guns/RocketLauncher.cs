using System.Collections;
using UnityEngine;

public class RocketLauncher : Gun
{
    // RocketLauncher parameter.
    public int rocketNum = 3;
    public float rocketAngle = 30;

    protected override void Fire()
    {
        animator.SetTrigger("Shoot");

        StartCoroutine(DelayFire(.2f));
    }

    IEnumerator DelayFire(float delay)
    {
        yield return new WaitForSeconds(delay);

        int median = rocketNum / 2;
        for (int i = 0; i < rocketNum; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
            bullet.transform.position = muzzlePos.position;
            
            // Even or odd, then count and shoot.
            if(rocketNum % 2 == 1)
            {
                bullet.transform.right = Quaternion.AngleAxis(rocketAngle * (i - median), Vector3.forward) * direction;
            }
            else
            {
                bullet.transform.right = Quaternion.AngleAxis(rocketAngle * (i - median) + rocketAngle / 2, Vector3.forward) * direction;
            }

            bullet.GetComponent<Rocket>().SetTarget(mousePos);
        }
    }
}
