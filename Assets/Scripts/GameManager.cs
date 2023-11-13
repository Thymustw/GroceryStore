using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // The guns status parameter.
    public int maxBulletCount;
    public int numPreShoot;
    public int maxReboundTime;
    public float interval;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        // Set the guns status.
        maxBulletCount = 1;
        numPreShoot = 1;
        maxReboundTime = 1;
        interval = 1;
    }


    private void Update()
    {   
        // Scenechanger.
        if (SceneManager.GetActiveScene().name == "ChooseScene")
            if (Input.GetMouseButtonDown(0) && MouseDetect().collider.CompareTag("AttackBox"))
                SceneManager.LoadScene("SampleScene");

        UpgradeTheWeapon();
    }


    // Create a ray to detect the things.
    private RaycastHit2D MouseDetect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        
        return hit;
    }


    // A interface to bridge each units HP count.
    public void DamageCount(GameObject hurt)
    {
        Destroy(hurt);
    }


    // A small system for the weapon upgrade.
    private void UpgradeTheWeapon()
    {
        // For rebound.
        if(Input.GetKeyDown(KeyCode.P))
            maxReboundTime = maxReboundTime + 1;
        if(Input.GetKeyDown(KeyCode.L) && --maxReboundTime <= 1)
            maxReboundTime = 1;

        // For bullet num.
        if(Input.GetKeyDown(KeyCode.O))
            maxBulletCount = maxBulletCount + 1;
        if(Input.GetKeyDown(KeyCode.K) && --maxBulletCount <= 1)
            maxBulletCount = 1;

        // TODO:For num pre shoot.
    }
}
