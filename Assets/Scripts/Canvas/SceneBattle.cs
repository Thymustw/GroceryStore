using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneBattle : MonoBehaviour
{
    Text statusText;
    Image upgradePanel;
    Button checkButton;

    Dictionary<string, object> FirstDict;
    Dictionary<string, object> SecondDict;
    Dictionary<string, object> ThirdDict;
    Dictionary<string, object> SelectDict;

    void Awake()
    {
        statusText = transform.GetChild(0).GetComponent<Text>();
        statusText.text = "";
        statusText.gameObject.SetActive(false);

        upgradePanel = transform.GetChild(1).GetComponent<Image>();

        checkButton = upgradePanel.transform.GetChild(3).GetComponent<Button>();

        FirstDict = GetUpgrade("First");
        SecondDict = GetUpgrade("Second");
        ThirdDict = GetUpgrade("Third");
    }

    void Start()
    {
        if (FirstDict.Count != 0 && SecondDict.Count != 0 && ThirdDict.Count != 0)
        {
            //three choose
            List<Button> buttons = new List<Button>();
            for (int i = 0; i < 3; i++)
            {
                Button tempButton = upgradePanel.transform.GetChild(i).GetComponent<Button>();
                tempButton.gameObject.SetActive(true);

                switch(i + 1)
                {
                    case 1:
                        tempButton.transform.GetChild(0).GetComponent<Text>().text = PrintStatus(FirstDict);
                        break;
                    case 2:
                        tempButton.transform.GetChild(0).GetComponent<Text>().text = PrintStatus(SecondDict);
                        break;
                    case 3:
                        tempButton.transform.GetChild(0).GetComponent<Text>().text = PrintStatus(ThirdDict);
                        break;
                }
                buttons.Add(tempButton);
            }
        }  
        else if (FirstDict.Count != 0 && SecondDict.Count == 0 && ThirdDict.Count == 0)
        {
            SelectDict = FirstDict;
            statusText.gameObject.SetActive(true);
            statusText.text = PrintStatus(FirstDict);
            StartCoroutine(AppearCheckButton());
        }
        GameManager.Instance.SetIndexOfTheCurrentUpgrade(GameManager.Instance.GetIndexOfTheCurrentUpgrade() + 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GameManager.Instance.ReactAll();
    }

    Dictionary<string, object> GetUpgrade(string what)
    {
        Dictionary<string, object> keyValues = new Dictionary<string, object>();

        List<Valve> temp = new List<Valve>();
        switch(what)
        {
            case "First":
            {
                temp = JsonReader.Instance.GetCurrentUpgradeOfFirst(GameManager.Instance.GetIndexOfTheCurrentUpgrade());
                break;
            }
            case "Second":
            {
                temp = JsonReader.Instance.GetCurrentUpgradeOfSecond(GameManager.Instance.GetIndexOfTheCurrentUpgrade());
                break;
            }
            case "Third":
            {
                temp = JsonReader.Instance.GetCurrentUpgradeOfThird(GameManager.Instance.GetIndexOfTheCurrentUpgrade());
                break;
            }
        }
        foreach(Valve valve in temp)
        {
            if (valve.AddATK != 0) keyValues.Add("AddATK", valve.AddATK);
            if (valve.MinusRate != 0) keyValues.Add("MinusRate", valve.MinusRate);
            if (valve.AddBullet != 0) keyValues.Add("AddBullet", valve.AddBullet);
            if (valve.AddRebound != 0) keyValues.Add("AddRebound", valve.AddRebound);
            if (valve.AddPreShootNum != 0) keyValues.Add("AddPreShootNum", valve.AddPreShootNum);
            if (valve.HitToSpilt) keyValues.Add("HitToSpilt", valve.HitToSpilt);
            if (valve.ChaseBullet) keyValues.Add("ChaseBullet", valve.ChaseBullet);
            if (valve.ThrougntEnemy) keyValues.Add("ThrougntEnemy", valve.ThrougntEnemy);
            if (valve.EnemyDeadAndShootTenCrossBullet) keyValues.Add("EnemyDeadAndShootTenCrossBullet", valve.EnemyDeadAndShootTenCrossBullet);
        }

        return keyValues;
    }

    public void ChooseUpgrade(string upgrade)
    {
        switch(upgrade)
        {
            case "First":
            {
                SelectDict = FirstDict;
                break;
            }
            case "Second":
            {
                SelectDict = SecondDict;
                break;
            }
            case "Third":
            {
                SelectDict = ThirdDict;
                break;
            }
        }
        checkButton.gameObject.SetActive(true);
    }

    string PrintStatus(Dictionary<string, object> dict)
    {
        string temp = "";
        foreach(string tempString in dict.Keys)
            temp += tempString + " : " + dict[tempString] + "\n";
        return temp;
    }

    void SendToGM(Dictionary<string, object> dict)
    {
        PlayerStats player = GameManager.Instance.GetPlayer();
        foreach (string i in dict.Keys)
        {
            switch(i)
            {
                case "AddATK":
                    player.SetCurrentDamage(player.GetCurrentDamage() + Convert.ToSingle(dict[i])); 
                    break;
                case "MinusRate":
                    player.SetCurrentIntervalPreShoot(player.GetCurrentIntervalPreShoot() - Convert.ToSingle(dict[i])); 
                    break;
                case "HitToSpilt":
                    GameManager.Instance.EnableHitToSpilt(Convert.ToBoolean(dict[i]));
                    break;
                case "ChaseBullet":
                    GameManager.Instance.EnableChaseBullet(Convert.ToBoolean(dict[i]));
                    break;
                case "AddRebound":
                    player.SetCurrentReboundTime(player.GetCurrentReboundTime() + Convert.ToInt16(dict[i]));
                    break;
                case "ThrougntEnemy":
                    GameManager.Instance.EnableThrougntEnemy(Convert.ToBoolean(dict[i]));
                    break;
                case "EnemyDeadAndShootTenCrossBullet":
                    GameManager.Instance.EnableEnemyDeadAndShootTenCrossBullet(Convert.ToBoolean(dict[i]));
                    break;
                case "AddBullet":
                    player.SetCurrentBulletCount(player.GetCurrentBulletCount() + Convert.ToInt16(dict[i]));
                    break;
                case "AddPreShootNum":
                    player.SetCurrentNumPreShootBullet(player.GetCurrentNumPreShootBullet() + Convert.ToInt16(dict[i]));
                    break;
                //TODO:升级代补
            }
        }
    }

    public void CheckButtonReactAll()
    {
        GameManager.Instance.ReactAll();
        SendToGM(SelectDict);
        statusText.gameObject.SetActive(false);
        upgradePanel.gameObject.SetActive(false);
    }

    IEnumerator AppearCheckButton()
    {
        yield return new WaitForSeconds(1f);
        checkButton.gameObject.SetActive(true);
    }
}
