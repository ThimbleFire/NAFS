using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
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

    public Item.Behaviour UseTool(Vector3 lastDirection)
    {
        if (ItemUseAnimation.InMotion)
            return Item.Behaviour.NONE;

        playerCharacterAnimator.SetTrigger("attack");
        return toolAnimator.StartAnimation(lastDirection);
    }
}
