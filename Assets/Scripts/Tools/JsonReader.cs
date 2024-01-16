using System.Collections.Generic;
using UnityEngine;
using System.IO;

#region "GunUpgrade"
[System.Serializable]
public class Valve
{
    public bool IsBanBooGun;
    public float AddATK;
    public float AddMaxHealth;
    public float AddBulletSpeed;
    public float MinusRate;
    //public float MinusBulletRate;
    public float TimesATKPersentage;
    public float TimesRunSpeed;
    public int AddBullet;
    public int AddRebound;
    public int AddPreShootNum;
    public bool HitToSpilt;
    public bool ChaseBullet;
    public bool EnemyDeadAndShootCrossBullet;
    public bool EnemyDeadAndShootTenCrossBullet;
}

[System.Serializable]
class Upgrade
{
    public List<Valve> First;
    public List<Valve> Second;
    public List<Valve> Third;
}

[System.Serializable]
class Root
{
    public List<Upgrade> Upgrade;
}
#endregion


#region "EnemyPool"
[System.Serializable]
class RoomLevel
{
    public int BullyingGuy;
    public int WorkSheet;
    public int Clock;
}

[System.Serializable]
class EnemyPoolRoot
{
    public List<RoomLevel> RoomLevel;
}
#endregion

public class JsonReader : MonoBehaviour
{
    private static JsonReader instance;
    public static JsonReader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new JsonReader();
            }
            return instance;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    // Start is called before the first frame update
    public void PrintAll()
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/GameData/Guns Upgrade Data/BanbooGunUpgrade.json");
        Root root = JsonUtility.FromJson<Root>(jsonData);
        //root.Upgrade.ForEach(x => Debug.Log(x.FirstData.First().ATK));

        int i = 0;
        foreach (Upgrade temp in root.Upgrade)
        {
            i++;
            Debug.Log("======="+ i +"=======");
            Debug.Log("=======First=======");
            foreach (Valve tempFirst in temp.First)
            {
                if (tempFirst.AddATK != 0) Debug.Log("加攻擊力:" + tempFirst.AddATK);
                if (tempFirst.MinusRate != 0) Debug.Log("加x射速:" + tempFirst.MinusRate);
                if (tempFirst.AddBullet != 0) Debug.Log("加x子彈:" + tempFirst.AddBullet);
                if (tempFirst.AddRebound != 0) Debug.Log("加x反彈:" + tempFirst.AddRebound);
                if (tempFirst.AddPreShootNum != 0) Debug.Log("加x連發:" + tempFirst.AddPreShootNum);
                if (tempFirst.HitToSpilt) Debug.Log("子彈撞擊敵人分裂:" + tempFirst.HitToSpilt);
                if (tempFirst.ChaseBullet) Debug.Log("追蹤子彈:" + tempFirst.ChaseBullet);
                if (tempFirst.EnemyDeadAndShootCrossBullet) Debug.Log("穿透敵人:" + tempFirst.EnemyDeadAndShootCrossBullet);
                if (tempFirst.EnemyDeadAndShootTenCrossBullet) Debug.Log("死亡後生成十字子彈:" + tempFirst.EnemyDeadAndShootTenCrossBullet);
            }
            Debug.Log("=======Second=======");
            foreach (Valve tempSecond in temp.Second)
            {
                if (tempSecond.AddATK != 0) Debug.Log("加攻擊力:" + tempSecond.AddATK);
                if (tempSecond.MinusRate != 0) Debug.Log("加x射速:" + tempSecond.MinusRate);
                if (tempSecond.AddBullet != 0) Debug.Log("加x子彈:" + tempSecond.AddBullet);
                if (tempSecond.AddRebound != 0) Debug.Log("加x反彈:" + tempSecond.AddRebound);
                if (tempSecond.AddPreShootNum != 0) Debug.Log("加x連發:" + tempSecond.AddPreShootNum);
                if (tempSecond.HitToSpilt) Debug.Log("子彈撞擊敵人分裂:" + tempSecond.HitToSpilt);
                if (tempSecond.ChaseBullet) Debug.Log("追蹤子彈:" + tempSecond.ChaseBullet);
                if (tempSecond.EnemyDeadAndShootCrossBullet) Debug.Log("穿透敵人:" + tempSecond.EnemyDeadAndShootCrossBullet);
                if (tempSecond.EnemyDeadAndShootTenCrossBullet) Debug.Log("死亡後生成十字子彈:" + tempSecond.EnemyDeadAndShootTenCrossBullet);
            }
            Debug.Log("=======Third=======");
            foreach (Valve tempThird in temp.Third)
            {
                if (tempThird.AddATK != 0) Debug.Log("加攻擊力:" + tempThird.AddATK);
                if (tempThird.MinusRate != 0) Debug.Log("加x射速:" + tempThird.MinusRate);
                if (tempThird.AddBullet != 0) Debug.Log("加x子彈:" + tempThird.AddBullet);
                if (tempThird.AddRebound != 0) Debug.Log("加x反彈:" + tempThird.AddRebound);
                if (tempThird.AddPreShootNum != 0) Debug.Log("加x連發:" + tempThird.AddPreShootNum);
                if (tempThird.HitToSpilt) Debug.Log("子彈撞擊敵人分裂:" + tempThird.HitToSpilt);
                if (tempThird.ChaseBullet) Debug.Log("追蹤子彈:" + tempThird.ChaseBullet);
                if (tempThird.EnemyDeadAndShootCrossBullet) Debug.Log("穿透敵人:" + tempThird.EnemyDeadAndShootCrossBullet);
                if (tempThird.EnemyDeadAndShootTenCrossBullet) Debug.Log("死亡後生成十字子彈:" + tempThird.EnemyDeadAndShootTenCrossBullet);
            }
        }
    }

    public List<Valve> GetCurrentUpgradeOfFirst(int index)
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/GameData/Guns Upgrade Data/BanbooGunUpgrade.json");
        Root root = JsonUtility.FromJson<Root>(jsonData);

        if (root.Upgrade[index].First != null)
            return root.Upgrade[index].First;
        else return null;
    }

    public List<Valve> GetCurrentUpgradeOfSecond(int index)
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/GameData/Guns Upgrade Data/BanbooGunUpgrade.json");
        Root root = JsonUtility.FromJson<Root>(jsonData);

        if (root.Upgrade[index].Second != null)
            return root.Upgrade[index].Second;
        else return null;
    }

    public List<Valve> GetCurrentUpgradeOfThird(int index)
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/GameData/Guns Upgrade Data/BanbooGunUpgrade.json");
        Root root = JsonUtility.FromJson<Root>(jsonData);

        if (root.Upgrade[index].Third != null)
            return root.Upgrade[index].Third;
        else return null;
    }


    public void GetCurrentEnemyPool(int index, Transform parent)
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/GameData/Enemy Generator Data/EnemyGenerator.json");
        EnemyPoolRoot enemyPoolRoot = JsonUtility.FromJson<EnemyPoolRoot>(jsonData);

        if (enemyPoolRoot.RoomLevel[index].BullyingGuy > 0)
            GenerateEnemy(enemyPoolRoot.RoomLevel[index].BullyingGuy, parent, "BullyingGuy");
        if (enemyPoolRoot.RoomLevel[index].WorkSheet > 0)
            GenerateEnemy(enemyPoolRoot.RoomLevel[index].WorkSheet, parent, "WorkSheet");
        if (enemyPoolRoot.RoomLevel[index].Clock > 0)
            GenerateEnemy(enemyPoolRoot.RoomLevel[index].WorkSheet, parent, "Clock");
        //TODO:后续补充所有敌人。
    }

    void GenerateEnemy(int sum, Transform parent, string prefabName)
    {
        for (int i = 0; i < sum; i++)
            {
                Vector2 tempPosition = new Vector2(Random.Range(-5.5f, 5.5f), Random.Range(-3.5f, 3.5f));
                while (Physics2D.OverlapCircle(tempPosition, 2f, 1<<7) && Physics2D.OverlapCircle(tempPosition, 2f, 1<<8))
                {
                    Debug.Log("retry");
                    tempPosition = new Vector2(Random.Range(-5.5f, 5.5f), Random.Range(-3.5f, 3.5f));
                }
                GameObject tempObj = Instantiate(Resources.Load("Prefabs/Enemy/" + prefabName) as GameObject, parent);
                tempObj.transform.position = tempPosition;
            }
    }
}
