using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement parameter.
    private Vector2 input;
    private Rigidbody2D rigidbody;
    private Animator animator;

    // Shoot parameter.
    private Vector2 mousePos;
    public GameObject[] guns;
    private int gunNum;

    // Unit status parameter.
    private PlayerStats playerStats;
    
    private void Awake() 
    {
        // Get component.
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();

        transform.position = new Vector2(0, 0);
        gunNum = playerStats.GetGunNumber();
        guns[gunNum].SetActive(true);
    }

    private void Start()
    {
        GameManager.Instance.RigisterPlayer(playerStats);
        GameManager.Instance.AddWaitGameObjectAndSetActiveFalse(gameObject);
    }

    private void Update() 
    {
        // This func just for Debuging. 
        //SwitchGun();

        // Get the keyboard input.
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Let velocity become input * speed.
        rigidbody.velocity = input.normalized * GameManager.Instance.RunSpeed();
        // Get your mouse position on the screen.
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    
        // The simple StateMachine for change the move state.
        if(input != Vector2.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);


        // Detect turn left or right, and filp it.
        if (mousePos.x > transform.position.x)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }

    // This func just for Debuging.
    /*private void SwitchGun()
    {
        // Press Q to change gun.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            guns[gunNum].SetActive(false);
            if(--gunNum < 0)
            {
                gunNum = guns.Length - 1;
            }
            guns[gunNum].SetActive(true);
        }

        // Press E to change gun.
        if (Input.GetKeyDown(KeyCode.E))
        {
            guns[gunNum].SetActive(false);
            if(++gunNum > guns.Length - 1)
            {
                gunNum = 0;
            }
            guns[gunNum].SetActive(true);
        }
    }*/
}
