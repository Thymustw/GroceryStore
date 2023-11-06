using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy perameter
    private Rigidbody2D rigidbody;
    private Animator animator;
    private Vector2 chasePos;
    

    private void Awake() 
    {
        // Get component.
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void Update() 
    {
        // The simple StateMachine for change the move state.
        // TODO:Makes it into real StateMachine.
        /*if(input != Vector2.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);*/


        // Detect turn left or right, and filp it.
        chasePos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (chasePos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }
}
