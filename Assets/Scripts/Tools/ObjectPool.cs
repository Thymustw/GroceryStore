using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool
{
    // Singleton for normal class.
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }

    // Create a dic to save <object, a mount of object>.
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    // ObjectPool
    private GameObject pool;


    // Regedit the object.
    public GameObject GetObject(GameObject prefab)
    {
        // Check the prefab is exist or not, BEFORE take (dequene) it.
        GameObject _object;
        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);

            // Check the objectpool exist or not.
            if (pool == null)
                pool = new GameObject("ObjectPool");

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
}