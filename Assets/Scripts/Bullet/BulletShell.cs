using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShell : MonoBehaviour
{
    // Shell parameter.
    public float speed;
    public float stopTime = .5f;
    public float fadeTime = .1f;
    new private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;


    private void Awake() 
    {
        // Get component.
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() 
    {
        // Let shell eject out.
        float shellAngel = Random.Range(-30f, 30f);
        rigidbody.velocity = Quaternion.AngleAxis(shellAngel, Vector3.forward) * Vector3.up * speed;

        // Reset the status.
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        rigidbody.gravityScale = 3f;
        
        // Disappear it.
        StartCoroutine(Stop());
    }


    IEnumerator Stop()
    {
        // Set and start stop time.
        yield return new WaitForSeconds(stopTime);

        // Clean rigidbody.
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0;


        // Fade the shell out.
        while(sprite.color.a > 0)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,sprite.color.a - fadeTime);
            yield return new WaitForFixedUpdate();
        }

        //Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }
}
