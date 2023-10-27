using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : _Gun
{
    protected override void Fire()
    {
        animator.SetTrigger("Shoot");

        RaycastHit2D hit2D = Physics2D.Raycast(muzzlePos.position, direction, 30);

        GameObject bullet = _ObjectPool.Instance.GetObject(bulletPrefab);
        LineRenderer tracer = bullet.GetComponent<LineRenderer>();
        tracer.SetPosition(0, muzzlePos.position);
        tracer.SetPosition(1, hit2D.point);

        GameObject shell = _ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }
}
