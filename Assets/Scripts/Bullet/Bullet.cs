using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet parameter.
    public float speed;
    new private Rigidbody2D rigidbody;
    new int reboundTime;
    new private Vector2 preDirection;

    //      __Prefab
    public GameObject explosionPrefab;
    

    private void Awake() 
    {
        // Get component.
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnEnable() 
    {
        // Recount when use it.
        reboundTime = 0;
    }


    // Detect the THINGS and do func.
    // Use OnCollisionEnter2D for the "normal".
    private void OnCollisionEnter2D(Collision2D other) 
    {
        
        if (other.collider.CompareTag("Enemy"))
        {
            HitColl();
            GameManager.Instance.DamageCount(other.gameObject);
        }
        if (other.collider.CompareTag("Wall"))
        {   
            if(++reboundTime < GameManager.Instance.maxReboundTime)
                Rebound(other);
            else
                HitColl();
        }
    }


    // Trigger then explosion and vanish. 
    /*private void OnTriggerEnter2D(Collider2D other) 
    {
        //Debug.Log((rigidbody.velocity).normalized);
        //拿他和reflect當作判別sincos
        /*Vector2 inVelocity = (rigidbody.velocity).normalized * -1;
        if (inVelocity.x < 1 && inVelocity.x > 0)
        {
            Vector2 reflectVelocity = Vector2.Reflect((rigidbody.velocity).normalized * -1, other.transform.right);
        }
        
        Debug.Log(-1 * (rigidbody.velocity).normalized);
        if (other.CompareTag("Enemy"))
        {
            HitColl();
            print(other.gameObject.name);
            GameManager.Instance.DamageCount(other.gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            

            print("hi");

            //HitColl();
        }
    }*/


    // Let bullet get the direction.
    public void SetSpeed(Vector2 direction)
    {
        preDirection = direction;
        rigidbody.velocity = preDirection * speed;
    }


    // If hit coll, then push object to the objectpool.
    private void HitColl()
    {
        //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;
            
        //Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }


    // Count the rebound vector.
    private void Rebound(Collision2D other)
    {
        Vector2 newDirection = Vector2.Reflect(preDirection, other.contacts[0].normal);
        rigidbody.velocity = newDirection * speed;
        preDirection = newDirection;
    }
}
