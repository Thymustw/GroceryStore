using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet parameter.
    public float speed;
    new private Rigidbody2D rigidbody;

    //      __Prefab
    public GameObject explosionPrefab;


    private void Awake() 
    {
        // Get component.
        rigidbody = GetComponent<Rigidbody2D>();
    }


    // Trigger then explosion and vanish. 
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;
        
        //Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }


    // Let bullet get the direction.
    public void SetSpeed(Vector2 direction)
    {
        rigidbody.velocity = direction * speed;
    }
}
