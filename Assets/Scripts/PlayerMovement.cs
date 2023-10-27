using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    private Vector2 input;
    private Animator animator;
    private Rigidbody2D rigidbody;
    
    private void Awake() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() { 
        //Get the keyboard input.
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        //Let velocity equals Vector2.input's nor (0~1,0~1) multiply by speed.
        rigidbody.velocity = input.normalized * speed;
    
        //The simple StateMachine for change the move state.
        //TODO:Makes it into real StateMachine.
        if(input != Vector2.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }
}
