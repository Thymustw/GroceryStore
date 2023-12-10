using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemsManager : Singleton<ItemsManager>
{
    private Array items;
    
    protected override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += OnSceneLoaded;
        //SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        items = GameObject.FindGameObjectsWithTag("Item");
    }

    /*void OnSceneUnloaded(Scene scene)
    {

    }*/


    public float GetTotalPlusBulletSpeed()
    {
        float valve = 0;
        if (items != null)
            foreach (GameObject item in items)
                valve += item.GetComponent<ItemStats>().itemData.plusBulletSpeed;
        return valve;
    }

    public float GetTotalPlusBulletDamage()
    {
        float valve = 0;
        if (items != null)
            foreach (GameObject item in items)
                valve += item.GetComponent<ItemStats>().itemData.plusBulletDamage;
        return valve;

    }

    public int GetTotalPlusBulletCount()
    {
        int valve = 0;
        if (items != null)
            foreach (GameObject item in items)
                valve += item.GetComponent<ItemStats>().itemData.plusBulletCount;
        return valve;
    }

    public int GetTotalPlusReboundTime()
    {
        int valve = 0;
        if (items != null)
            foreach (GameObject item in items)
                valve += item.GetComponent<ItemStats>().itemData.plusReboundTime;
        return valve;
    }

    public int GetTotalPlusNumPreShootBullet()
    {
        int valve = 0;
        if (items != null)
            foreach (GameObject item in items)
                valve += item.GetComponent<ItemStats>().itemData.plusNumPreShootBullet;
        return valve;
    }

    public float GetTotalMinusIntervalPreShoot()
    {
        float valve = 0;
        if (items != null)
            foreach (GameObject item in items)
                valve += item.GetComponent<ItemStats>().itemData.minusIntervalPreShoot;
        return valve;
    }

    public float GetTotalMinusIntervalPreBullet()
    {
        float valve = 0;
        if (items != null)
            foreach (GameObject item in items)
                valve += item.GetComponent<ItemStats>().itemData.minusIntervalPreBullet;
        return valve;
    }

    public float GetTotalTimesDamagePersentage()
    {
        float valve = 1;
        if (items != null)
            foreach (GameObject item in items)
                valve *= item.GetComponent<ItemStats>().itemData.timesDamagePersentage;
        return valve;
    }

    public float GetTotalTimesBulletSize()
    {
        float valve = 1;
        if (items != null)
            foreach (GameObject item in items)
                valve *= item.GetComponent<ItemStats>().itemData.timesBulletSize;
        return valve;
    }
}