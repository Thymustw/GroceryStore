using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Rocket parameter.
    public float lerp;
    public float speed = 15f;
    public GameObject explosionPrefab;
    private Rigidbody2D rigidbody;
    private bool arrived;

    private Vector3 targetPos;
    private Vector3 direction;

    private void Awake() 
    {
        // Get component.
        rigidbody = GetComponent<Rigidbody2D>();
    }


    // Func to set the rocket terminal.
    public void SetTarget(Vector2 _target)
    {
        arrived = false;
        targetPos = _target;
    }


    private void FixedUpdate()
    {
        direction = (targetPos - transform.position).normalized;

        // Do the half circular motion until to the terminal.
        if(!arrived)
        {
            transform.right = Vector3.Slerp(transform.right, direction, lerp / Vector3.Distance(transform.position, targetPos));
            rigidbody.velocity = transform.right * speed;
        }
        if(Vector2.Distance(transform.position, targetPos) < 1f && !arrived)
        {
            arrived = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject explosion = ObjectPool.Instance.GetObject(explosionPrefab);
        explosion.transform.position = transform.position;
        
        // Let rocket vanish slower.
        rigidbody.velocity = Vector2.zero;
        StartCoroutine(Push(gameObject, .3f));
    }


    IEnumerator Push(GameObject _object, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.Instance.PushObject(_object);
    }
}
