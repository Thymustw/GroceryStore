using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet parameter.
    private float speed;
    new private Rigidbody2D rigidbody;
    int reboundTime;
    private Vector2 preDirection;
    private bool hasBeenSpilt;
    //private int maxSpiltBulletCount = 2;

    //      __Prefab
    public GameObject explosionPrefab;
    //public GameObject bulletPrefab;
    private PlayerStats playerStats;
    

    private void Awake() 
    {
        // Get component.
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() 
    {
        // Recount when use it.
        reboundTime = 0;
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        speed = GameManager.Instance.BulletSpeed();
    }


    private void Update()
    {
        transform.right = rigidbody.velocity;
    }

    // Detect the THINGS and do func.
    // Use OnCollisionEnter2D for the "normal".
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlayAudio(new IPlayAudioEnemyHit());
            HitColl(other.contacts[0].point);
            //TODO:Change to correct func.
            float damage = GameManager.Instance.BulletDamage();
            GameManager.Instance.DamageCount(other.gameObject, damage);

            //TODO:Write the HitToSpilt
            /*if(GameManager.Instance.GetHitToSpilt() && !hasBeenSpilt)
            {
                int median = maxSpiltBulletCount / 2;
                Vector2 enemyCenter = new Vector2(other.transform.position.x - other.contacts[0].point.x, other.transform.position.y - other.contacts[0].point.y);
                
                for(int i = 0; i < maxSpiltBulletCount; i++)
                {
                    GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);

                    //Vector2 bulletCenter;
                    //Vector2 generatorCenter;
                    Vector2 direction = enemyCenter.normalized;

                    bullet.transform.position = other.transform.position;
                    bullet.transform.localScale = new Vector3 (0.75f, 0.75f);
                    bullet.GetComponent<Bullet>().SetHasBeenSpilt(true);

                    float randomAngle = Random.Range(-2f, 2f);
                    if(maxSpiltBulletCount % 2 == 1)
                    {
                        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(10 * (i - median) + randomAngle, Vector3.forward) * direction);
                    }
                    else
                    {
                        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(10 * (i - median) + 10 / 2 + randomAngle, Vector3.forward) * direction);
                    }
                }
            }*/
        }
        if (other.collider.CompareTag("Wall"))
        {   
            if(++reboundTime < GameManager.Instance.MaxReboundTime())
                Rebound(other);
            else
                HitColl(other.contacts[0].point);
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
    private void HitColl(Vector2 hitPoint)
    {
        //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = hitPoint;
            
        //Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);
    }

    /*public bool GetHasBeenSpilt()
    {
        return hasBeenSpilt;
    }

    public void SetHasBeenSpilt(bool state)
    {
        hasBeenSpilt = state;
    }*/


    // Count the rebound vector.
    private void Rebound(Collision2D other)
    {
        Vector2 newDirection = Vector2.Reflect(preDirection, other.contacts[0].normal);
        rigidbody.velocity = newDirection * speed;
        preDirection = newDirection;
    }
}
