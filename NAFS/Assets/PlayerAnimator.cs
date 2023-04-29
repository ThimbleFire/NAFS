using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator playerCharacterAnimator;
    public Animator toolAnimator;

    private void Awake()
    {
        Inventory.OnActiveItemChangeFromPickup += Inventory_OnActiveItemChangeFromPickup;
    }

    public void UpdateVelocity(Vector3 velocity)
    {
        bool idle = velocity.Equals(Vector3.zero);

        playerCharacterAnimator.SetBool("idle", idle);
        toolAnimator.SetBool("idle", idle);

        if (idle)
            return;

        playerCharacterAnimator.SetFloat("velx", velocity.x);
        playerCharacterAnimator.SetFloat("vely", velocity.y);
        toolAnimator.SetFloat("velx", velocity.x);
        toolAnimator.SetFloat("vely", velocity.y);
    }

    internal void UseTool()
    {
        playerCharacterAnimator.SetTrigger("attack");
        toolAnimator.SetTrigger("attack");
    }

    private void Inventory_OnActiveItemChangeFromPickup(string animationControllerOverrideFileName)
    {
        toolAnimator.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>(animationControllerOverrideFileName);
    }
}
