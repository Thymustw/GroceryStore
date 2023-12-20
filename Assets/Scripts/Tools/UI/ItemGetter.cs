using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetter : Singleton<ItemGetter>
{
    private Text textWin;
    protected override void Awake()
    {
        base.Awake();
        textWin = GameObject.Find("WinText").GetComponent<Text>();
        GameManager.Instance.RigisterItemGetter(this);
    }

    void SetText(GameObject item)
    {
        
    }
}
