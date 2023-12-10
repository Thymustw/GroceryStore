using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action<float, float> UpdateHealthOnHurt;
    private PlayerStats playerStats;

    protected override void Awake()
    {
        base.Awake();

        SceneManager.activeSceneChanged += OnSceneChanged;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnSceneChanged(Scene scene, Scene nextScene)
    {
        // Find player stats.
        if(nextScene.name == "BattleScene")
        {
            playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
            UpdateHealthOnHurt?.Invoke(playerStats.GetCurrentHealth(), playerStats.GetMaxHealth());
        }
        else if(nextScene.name == "ChooseScene")
        {
            ObjectPool.Instance.GetTheChild();
        }
        else
        {
            playerStats = null;
        }
    }

    void OnSceneUnloaded(Scene scene)
    {
        StopAllCoroutines();
    }

    private void Update()
    {   
        // Scenechanger.
        if (SceneManager.GetActiveScene().name == "ChooseScene")
            try
            {
                if (Input.GetMouseButtonDown(0) && MouseDetect().collider.CompareTag("AttackBox"))
                    if(MouseDetect().collider.GetComponent<SpriteRenderer>().color != Color.white)
                    {
                        print("not change");
                        MouseDetect().collider.GetComponent<SpriteRenderer>().color = Color.white;
                        SceneManager.LoadScene("DialogueScene");
                    }
            }
            catch (Exception e){}
        else if (SceneManager.GetActiveScene().name == "BattleScene")
            StartCoroutine(PassBattleTime(5));


        //UpgradeTheWeapon();
    }

    IEnumerator PassBattleTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("ChooseScene");
    }


    // Create a ray to detect the things.
    private RaycastHit2D MouseDetect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        
        return hit;
    }


    // A interface to bridge each units HP count.
    public void DamageCount(GameObject hurt, float damage)
    {
        if (hurt.CompareTag("Player"))
        {
            var player = hurt.GetComponent<PlayerStats>();
            var HP = player.GetCurrentHealth();
            
            player.SetCurrentHealth(Mathf.Max(0, HP -= damage));
            print(player.GetCurrentHealth());
            UpdateHealthOnHurt?.Invoke(player.GetCurrentHealth(), player.GetMaxHealth());
        }
        else
        {
            var enemy = hurt.GetComponent<EnemyStats>();
            var HP = enemy.GetCurrentHealth();
            
            enemy.SetCurrentHealth(Mathf.Max(0, HP -= damage));
        }
    }

    public int MaxBulletCount() => playerStats.GetMaxBulletCount() + ItemsManager.Instance.GetTotalPlusBulletCount();
    public int MaxReboundTime() => playerStats.GetMaxReboundTime() + ItemsManager.Instance.GetTotalPlusReboundTime();
    public int MaxNumPreShootBullet() => playerStats.GetNumPreShootBullet() + ItemsManager.Instance.GetTotalPlusNumPreShootBullet();
    public float IntervalPreShoot() => Mathf.Max(0, playerStats.GetIntervalPreShoot() - ItemsManager.Instance.GetTotalMinusIntervalPreShoot());
    public float IntervalPreBullet() => Mathf.Max(0.05f, playerStats.GetIntervalPreBullet() - ItemsManager.Instance.GetTotalMinusIntervalPreBullet());
    public float BulletSpeed() => playerStats.GetBulletSpeed() + ItemsManager.Instance.GetTotalPlusBulletSpeed();
    public float BulletDamage() => (playerStats.GetDamage() + ItemsManager.Instance.GetTotalPlusBulletDamage()) * ItemsManager.Instance.GetTotalTimesDamagePersentage();
    public float BulletSize(GameObject bullet) => bullet.transform.localScale.y * ItemsManager.Instance.GetTotalTimesBulletSize();


    // A small system for the weapon upgrade.
    /*private void UpgradeTheWeapon()
    {
        // For rebound.
        if(Input.GetKeyDown(KeyCode.P))
            attackStats.SetMaxReboundTime(attackStats.GetMaxReboundTime() + 1);
        if(Input.GetKeyDown(KeyCode.L))
            attackStats.SetMaxReboundTime(Mathf.Max(1, attackStats.GetMaxReboundTime() - 1));

        // For bullet num.
        if(Input.GetKeyDown(KeyCode.O))
            attackStats.SetMaxBulletCount(attackStats.GetMaxBulletCount() + 1);
        if(Input.GetKeyDown(KeyCode.K))
            attackStats.SetMaxBulletCount(Mathf.Max(1, attackStats.GetMaxBulletCount() - 1));

        // For num pre shoot.
        if(Input.GetKeyDown(KeyCode.I))
            attackStats.SetNumPreShootBullet(attackStats.GetNumPreShootBullet() + 1);
        if(Input.GetKeyDown(KeyCode.J))
            attackStats.SetNumPreShootBullet(Mathf.Max(1, attackStats.GetNumPreShootBullet() - 1));
    }*/
}