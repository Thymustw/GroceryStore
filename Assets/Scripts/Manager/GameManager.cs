using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    List<GameObject> objectList = new List<GameObject>();
    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();

    List<Color> chooseBox = new List<Color>();


    public List<GameObject> itemInputGameobjects = new List<GameObject>();
    public List<GameObject> itemOutputGameobjects = new List<GameObject>();
    public List<TextAsset> dialogueInputGameobjects = new List<TextAsset>();
    public List<TextAsset> dialogueOutputGameobjects = new List<TextAsset>();


    public event Action<float, float> UpdateHealthOnHurt;
    public event Action<int> UpdateDialogue;

    private DialogueStats dialogueStats;
    private PlayerStats playerStats;
    private ItemCollectorStats itemCollectorStats;
    private ItemsCollector itemsCollector;
    private ItemGetter itemGetter;

    private int indexOfTheCurrentEnemyPool = 0;
    private int indexOfTheCurrentUpgrade = 0;
    private int indexOfTheCurrentTextAssetAndItem;

    private float playerPreMaxHealth;

    bool enemyDeadAndShootCrossBullet, enemyDeadAndShootTenCrossBullet, isGetItem;

    SpriteRenderer hand;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        //SceneManager.activeSceneChanged += OnSceneChanged;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        itemCollectorStats = GetComponent<ItemCollectorStats>();
        dialogueStats = GetComponent<DialogueStats>();

        foreach (var item in itemCollectorStats.itemCollectorData.itemLibGameobjects)
        {
            itemInputGameobjects.Add(item);
        }

        foreach (var dialogue in dialogueStats.dialogueData.dialogueFileList)
        {
            dialogueInputGameobjects.Add(dialogue);
        }

        itemOutputGameobjects = new List<GameObject>();
        dialogueOutputGameobjects = new List<TextAsset>();

        hand = GetComponent<SpriteRenderer>();
        // itemCollectorStats = GetComponent<ItemCollectorStats>();
        // if (itemInputGameobjects.Count == 0)
        //     itemInputGameobjects = itemCollectorStats.GetItemCollectorList();
        // foreach (var item in itemOutputGameobjects)
        //     print("itemOutputGameobjects" + item.name);
        // print("====================");
        // foreach (var item in itemInputGameobjects)
        //     print("itemInputGameobjects" + item.name);
        /*foreach(GameObject itemmmm in itemInputGameobjects)
        {
            Debug.Log(itemmmm.name);
        }*/
    }

    /*void OnSceneChanged(Scene scene, Scene nextScene)
    {
        // Find player stats.
        if(nextScene.name == "BattleScene")
        {
            //playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
            //UpdateHealthOnHurt?.Invoke(playerStats.GetCurrentHealth(), playerStats.GetMaxHealth());
        }
        else if(nextScene.name == "ChooseScene")
        {
            //if (ObjectPool.Instance != null)
            //    ObjectPool.Instance.StopChildren();
        }
        else
        {
            playerStats = null;
        }
    }*/

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
                hand.color = new Color(1, 1, 1, 1);

                var mousePos = Input.mousePosition;
                mousePos.z = 10;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                
                hand.transform.position = mousePos;
                isGetItem = false;
                if (Input.GetMouseButtonDown(0) && MouseDetect().collider.CompareTag("AttackBox"))
                    if(MouseDetect().collider.GetComponent<SpriteRenderer>().color != Color.white)
                    {
                        MouseDetect().collider.GetComponent<SpriteRenderer>().color = Color.white;
                        AudioManager.Instance.PlayAudio(new IPlayAudioChooseBox());
                        StartCoroutine(Timer(0.5f));
                        SceneManager.LoadScene("DialogueScene");
                        indexOfTheCurrentTextAssetAndItem = UnityEngine.Random.Range(0, dialogueInputGameobjects.Count);
                    }
            }
            catch (Exception e){}
        else
            hand.color = new Color(1, 1, 1, 0);
        //else if (SceneManager.GetActiveScene().name == "BattleScene")
        //    StartCoroutine(PassBattleTime(20));

        //Player dead.
        if (playerStats && playerStats.GetCurrentHealth() == 0)
            NotifyObserversPlayerIsDead();

        if (playerStats && objectList.Count == 0 && endGameObservers.Count == 0)
        {
            ObjectPool.Instance.StopChildren();

            playerStats.gameObject.SetActive(false);
            itemGetter.gameObject.SetActive(true);

            if (!isGetItem)
            {
                SetInItems();
                isGetItem = true;
            }
            
            if(itemInputGameobjects.Count <= 0)
                StartCoroutine(EndingAfterTimer(2));
            else
                StartCoroutine(BattleAfterTimer(5));
            //itemGetter
        }

        //UpgradeTheWeapon();
    }


    #region "Rigister game"
    public void RigisterPlayer(PlayerStats player)
    {
        playerStats = player;

        if(playerPreMaxHealth == 0)
            playerPreMaxHealth = playerStats.GetBaseMaxHealth();

        playerStats.SetCurrentMaxHealth(MaxCurrentHealth());
        if (playerPreMaxHealth != playerStats.GetCurrentMaxHealth())
        {
            float addHealth = playerStats.GetCurrentMaxHealth() - playerPreMaxHealth;
            playerStats.SetCurrentHealth(playerStats.GetCurrentHealth() + addHealth);
            playerPreMaxHealth = playerStats.GetCurrentMaxHealth();
        }
    }

    public PlayerStats GetPlayer()
    {
        if (playerStats != null)
            return playerStats;
        else return null;
    }

    // public void RigisterItemStats(ItemCollectorStats itemCollector)
    // {
    //     itemCollectorStats = itemCollector;
    // }

    public void RigisterItem(ItemsCollector items)
    {
        itemsCollector = items;
    }

    public void RigisterItemGetter(ItemGetter itemgetter)
    {
        itemGetter = itemgetter;
        itemGetter.gameObject.SetActive(false);
    }

    public void AddWaitGameObjectAndSetActiveFalse(GameObject gameobject)
    {
        objectList.Add(gameobject);
        gameobject.SetActive(false);
    }
    #endregion

    #region "React game"
    public void ReactAll()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i]?.SetActive(true);
        }
        /*foreach (GameObject gameObject in objectList)
        {
            print(gameObject.name);
            gameObject?.SetActive(true);
        }*/
        objectList.Clear();
        UpdateHealthOnHurt?.Invoke(playerStats.GetCurrentHealth(), playerStats.GetCurrentMaxHealth());
    }

    public void ResentItemToItemGetter()
    {
        
    }
    #endregion

    #region "End game"
    public void AddEndGameObserver(IEndGameObserver observer)
    {
        endGameObservers.Add(observer);
    }

    public void RemoveEndGameObserver(IEndGameObserver observer)
    {
        endGameObservers.Remove(observer);
    }

    public void NotifyObserversPlayerIsDead()
    {
        foreach (var observer in endGameObservers)
        {
            observer.EndNotify();
        }
        playerStats.gameObject.SetActive(false);

        //SceneManager.LoadScene("StartScene");
        StartCoroutine(EndGame(2));
    }
    #endregion

    public TextAsset GetOutputTextAsset()
    {
        TextAsset dialogueGet = dialogueInputGameobjects[indexOfTheCurrentTextAssetAndItem];
        Debug.Log(dialogueGet.name);
        dialogueOutputGameobjects.Add(dialogueGet);

        dialogueInputGameobjects.RemoveAt(indexOfTheCurrentTextAssetAndItem);

        return dialogueGet;
    }
    public List<GameObject> GetOutputItems()
    {
        return new List<GameObject>(itemOutputGameobjects);
    }
    public void SetInItems()
    {
        if (itemCollectorStats != null)
        {
            //int index = UnityEngine.Random.Range(0, itemInputGameobjects.Count - 1);
            GameObject itemGet = itemInputGameobjects[indexOfTheCurrentTextAssetAndItem];
            AudioManager.Instance.PlayAudio(new IPlayAudioGetItem());
            //Debug.Log(itemGet.name);

            itemOutputGameobjects.Add(itemGet);
            itemGetter.SetItemThing(itemGet);

            itemInputGameobjects.RemoveAt(indexOfTheCurrentTextAssetAndItem);
        }
    }

    IEnumerator Timer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator BattleAfterTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AudioManager.Instance.PlayAudio(new IPlayAudioChangeScene());
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("ChooseScene");
        isGetItem = false;
    }

    IEnumerator EndingAfterTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        VideoManager.Instance.SetEnding(true);
        AudioManager.Instance.PlayAudio(new IPlayAudioChangeScene());
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("VideoScene");
        isGetItem = false;
    }

    public IEnumerator EndGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

    #region "tool"

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
            UpdateHealthOnHurt?.Invoke(player.GetCurrentHealth(), player.GetCurrentMaxHealth());
        }
        else
        {
            float enemyHP = hurt.GetComponent<FSM>().parameter.currentHealth;
            
            hurt.GetComponent<FSM>().parameter.currentHealth = Mathf.Max(0, enemyHP -= damage);
        }
    }

    #endregion

    #region "Getter"

    public int MaxBulletCount() => playerStats.GetCurrentBulletCount() + itemsCollector.GetTotalPlusBulletCount();
    public int MaxReboundTime() => playerStats.GetCurrentReboundTime() + itemsCollector.GetTotalPlusReboundTime();
    public int MaxNumPreShootBullet() => playerStats.GetCurrentNumPreShootBullet() + itemsCollector.GetTotalPlusNumPreShootBullet();
    public float IntervalPreShoot() => Mathf.Max(0, playerStats.GetCurrentIntervalPreShoot() - itemsCollector.GetTotalMinusIntervalPreShoot());
    public float IntervalPreBullet() => Mathf.Max(0.05f, playerStats.GetCurrentIntervalPreBullet() - itemsCollector.GetTotalMinusIntervalPreBullet());
    public float BulletSpeed() => playerStats.GetCurrentBulletSpeed() + itemsCollector.GetTotalPlusBulletSpeed();
    public float BulletDamage() => (playerStats.GetCurrentDamage() + itemsCollector.GetTotalPlusBulletDamage()) * itemsCollector.GetTotalTimesDamagePersentage();
    public float BulletSize(GameObject bullet) => bullet.transform.localScale.y * itemsCollector.GetTotalTimesBulletSize();
    public float RunSpeed() => playerStats.GetCurrentRunSpeed() * itemsCollector.GetTotalTimesRunSpeed();
    public float MaxCurrentHealth() => playerStats.GetBaseMaxHealth() + itemsCollector.GetPlusHealth();

    /*public void EnableHitToSpilt(bool value = false) {hitToSpilt = value;}
    public bool GetHitToSpilt() { return hitToSpilt ;}
    public void EnableChaseBullet(bool value = false) {chaseBullet = value;}
    public bool GetChaseBullet() { return chaseBullet ;}*/
    public void EnableEnemyDeadAndShootCrossBullet(bool value = false) {enemyDeadAndShootCrossBullet = value;}
    public bool GetEnemyDeadAndShootCrossBullet() { return enemyDeadAndShootCrossBullet ;}
    public void EnableEnemyDeadAndShootTenCrossBullet(bool value = false) {enemyDeadAndShootTenCrossBullet = value;}
    public bool GetEnemyDeadAndShootTenCrossBullet() { return enemyDeadAndShootTenCrossBullet ;}
    
    #endregion

    /*public int GetIndexOfTheCurrentTextAsset()
    {
        return indexOfTheCurrentTextAsset;
    }
    public void SetIndexOfTheCurrentTextAsset(int value)
    {
        indexOfTheCurrentTextAsset = value;
    }*/


    public int GetIndexOfTheCurrentUpgrade()
    {
        return indexOfTheCurrentUpgrade;
    }
    public void SetIndexOfTheCurrentUpgrade(int value)
    {
        indexOfTheCurrentUpgrade = value;
    }


    public int GetIndexOfTheEnemyPool()
    {
        return indexOfTheCurrentEnemyPool;
    }
    public void SetIndexOfTheEnemyPool(int value)
    {
        indexOfTheCurrentEnemyPool = value;
    }


    public List<Color> GetChooseBox()
    {
        return chooseBox;
    }
    public void SetChooseBox(List<Color> value)
    {
        chooseBox = value;
    }
}