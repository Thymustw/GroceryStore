using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    // Guns parameter.
    private Animator animator;
    private float flipY;
    
    //      __Bullet
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    private Transform muzzlePos;
    private Transform shellPos;

    //      __Timer
    private float timer;
    public float interval;

    //      __Mouse
    private Vector2 mousePos;
    private Vector2 direction;


    private void Awake() 
    {
        // Get component.
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");
        shellPos = transform.Find("BulletShell");

        flipY = transform.localScale.y;
    }


    private void Update() 
    {
        // Get mouse position.
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Let guns not to up side down.
        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(flipY, -flipY, 1);
        }
        else
        {
            transform.localScale = new Vector3(flipY, flipY, 1);
        }

        Shoot();
    }


    private void Shoot()
    {
        // Get shooting direction.
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;

        // Let gun's right become your shooting direction.      (if turn left, player will help it)
        transform.right = direction;


        // The timer for count down shoot interval.
        if(timer != 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
                timer = 0;
        }


        // Check if player can shoot or not.
        if(Input.GetButton("Fire1"))
        {
            if(timer == 0)
            {
                Fire();
                timer = interval;
            }
        }
    }


    private void Fire()
    {
        // Play anime.
        animator.SetTrigger("Shoot");

        // Shoot bullet from muzzle position.
        GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetSpeed(direction);

        // Eject the shell.
        Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
    }
}
