using System.Collections.Generic;
using UnityEngine;

public class ItemsCollector : MonoBehaviour
{
    private List<GameObject> items = new List<GameObject>();
    
    void Awake()
    {
        List<GameObject> itemsTemp = GameManager.Instance.GetOutputItems();
        foreach(var item in itemsTemp)
        {   
            GameObject temp = Instantiate(item, transform);
            items.Add(temp);
        }
        GameManager.Instance.RigisterItem(gameObject.GetComponent<ItemsCollector>());
    }

    void Start()
    {
        GameManager.Instance.AddWaitGameObjectAndSetActiveFalse(this.gameObject);
    }

    #region  "Get Value"
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
        //TODO:要確認。
        valve = Mathf.Max(1, valve);
        return valve;
    }

    public float GetTotalTimesBulletSize()
    {
        float valve = 1;
        if (items != null)
            foreach (GameObject item in items)
                valve *= item.GetComponent<ItemStats>().itemData.timesBulletSize;
        //TODO:要確認。
        valve = Mathf.Max(1, valve);
        return valve;
    }

    public float GetTotalTimesRunSpeed()
    {
        float valve = 1;
        if (items != null)
            foreach (GameObject item in items)
                valve *= item.GetComponent<ItemStats>().itemData.timesRunSpeed;
        //TODO:要確認。
        valve = Mathf.Max(1, valve);
        return valve;
    }

    public float GetPlusHealth()
    {
        float valve = 0;
        if (items != null)
            foreach (GameObject item in items)
                valve += item.GetComponent<ItemStats>().itemData.plusHealth;
        return valve;
    }
    #endregion
}