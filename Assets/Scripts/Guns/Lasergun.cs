using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasergun : Gun
{
    // Lasergun parameter.
    private bool isShooting;
    private LineRenderer laser;
    private ParticleSystem effect;


    protected override void Awake() 
    {
        base.Awake();
        // Get component.
        laser = muzzlePos.GetComponent<LineRenderer>();
        effect = GameObject.Find("Effect").GetComponent<ParticleSystem>();
    }


    protected override void FireCtrl()
    {   
        // Detect mouse.
        if(Input.GetButtonDown("Fire1"))
        {
            isShooting = true;
            laser.enabled = true;
            effect.Play();
        }
        if(Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
            laser.enabled = false;
            effect.Stop();
        }

        // Play anime.
        animator.SetBool("Shoot", isShooting);

        if(isShooting)
        {
            Fire();
        }
    }


    protected override void Fire()
    {
        // Get the hit thing.
        RaycastHit2D hit2D = Physics2D.Raycast(muzzlePos.position, direction, 30f);

        // Draw the laser line.
        Debug.DrawLine(muzzlePos.position, hit2D.point, Color.red);
        laser.SetPosition(0, muzzlePos.position);
        laser.SetPosition(1, hit2D.point);

        // Draw the particle.
        effect.transform.position = hit2D.point;
        effect.transform.forward = -direction;
    }
}
