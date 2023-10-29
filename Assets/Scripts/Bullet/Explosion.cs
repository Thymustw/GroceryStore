using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Explosion parameter.
    private Animator animator;
    private AnimatorStateInfo info;

    private void Awake() 
    {
        // Get component.
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        // Play anime and destory it.
        info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1.0f)
            //Destroy(gameObject);
            ObjectPool.Instance.PushObject(gameObject);
    }
}
