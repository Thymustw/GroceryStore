using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Guns parameter.
    protected Animator animator;
    private float flipY;
    
    //      __Bullet
    public GameObject bulletPrefab;
    public GameObject shellPrefab;
    protected Transform muzzlePos;
    protected Transform shellPos;
    // private float bulletAngel;

    //      __Timer
    protected float timer;
    public float interval;

    //      __Mouse
    protected Vector2 mousePos;
    protected Vector2 direction;


    protected virtual void Awake() 
    {
        // Get component.
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");
        shellPos = transform.Find("BulletShell");

        flipY = transform.localScale.y;
    }


    protected virtual void Update() 
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


    protected virtual void Shoot()
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
        FireCtrl();
    }


    protected virtual void FireCtrl()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(timer == 0)
            {
                Fire();
                timer = interval;
            }
        }
    }

    protected virtual void Fire()
    {
        // Play anime.
        animator.SetTrigger("Shoot");

        // Shoot bullet from muzzle position.
        //GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = muzzlePos.position;

        float angle = Random.Range(-5f, 5f);
        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angle, Vector3.forward) * direction);

        // Eject the shell.
        //Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }
}
