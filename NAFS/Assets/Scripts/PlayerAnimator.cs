using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator playerCharacterAnimator;
    public ItemUseAnimation toolAnimator;

    public void UpdateVelocity(Vector3 velocity)
    {
        bool idle = velocity.Equals(Vector3.zero);

        playerCharacterAnimator.SetBool("idle", idle);

        if (idle)
            return;

        playerCharacterAnimator.SetFloat("velx", velocity.x);
        playerCharacterAnimator.SetFloat("vely", velocity.y);
    }

    internal void UseTool(Vector3 lastDirection)
    {
        if (toolAnimator.InMotion)
            return;

        playerCharacterAnimator.SetTrigger("attack");
        toolAnimator.StartAnimation(lastDirection);
    }
}
