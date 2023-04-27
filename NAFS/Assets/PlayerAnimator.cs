using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    public void UpdateVelocity(Vector3 velocity)
    {
        bool idle = velocity.Equals(Vector3.zero);

        animator.SetBool("idle", idle);

        if (idle)
            return;

        Debug.Log(velocity);

        animator.SetFloat("velx", velocity.x);
        animator.SetFloat("vely", velocity.y);
    }
}
