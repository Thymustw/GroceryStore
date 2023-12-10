using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPool : Singleton<ObjectPool>
{
    // Create a dic to save <object, a mount of object>.
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    // ObjectPool
    private GameObject pool;

    protected override void Awake()
    {
        base.Awake();
        pool = this.gameObject;
        //SceneManager.activeSceneChanged += OnSceneChanged;
    }


    // Regedit the object.
    public GameObject GetObject(GameObject prefab)
    {
        // Check the prefab is exist or not, BEFORE take (dequene) it.
        GameObject _object;
        
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0 || objectPool[prefab.name].Peek().activeSelf)
        {
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);

            // Check the objectpool exist or not.
            //if (pool == null)
            //    pool = new GameObject("ObjectPool");

            // Check the namepool exist or not.
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (!childPool)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
            }

            _object.transform.SetParent(childPool.transform);
        }

        // Take one out.
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }


    // Find and use the object.
    public void PushObject(GameObject prefab)
    {
        string _name = prefab.name.Replace("(Clone)", string.Empty);
        // Check the name quene is exist or not.
        if(!objectPool.ContainsKey(_name))
            objectPool.Add(_name, new Queue<GameObject>());

        // Save in name quene.
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }

    /*void OnSceneChanged(Scene scene, Scene nextScene)
    {
        print(scene.name);
        if(scene.name == "BattleScene")
        {
            print("hi");
            GetTheChild();
        }
    }*/

    public void GetTheChild()
    {
        foreach(Transform child in pool.transform.GetComponentsInChildren<Transform>())
        {
            if(child.gameObject.name.Contains("(Clone)"))
                child.gameObject.SetActive(false);
        }
    }
}