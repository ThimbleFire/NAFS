using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseAnimation : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Animator animator;
    private Item.UseAnimation animationType;

    public void Awake()
    {
        Inventory.OnActiveItemChangeFromPickup += Inventory_OnActiveItemChangeFromPickup;
        Inventory.OnActiveItemChangeFromUnequip += Inventory_OnActiveItemChangeFromUnequip;

        renderer.enabled = false;
    }
    public void StartAnimation(Vector3 lastDirection)
    {
        if (animationType == Item.UseAnimation.NONE)
            return;

        renderer.enabled = true;
        animator.SetFloat("velx", lastDirection.x);
        animator.SetFloat("vely", lastDirection.y);
        animator.SetTrigger("attack");
    }

    public void EndAnimation()
    {
        renderer.enabled = false;
    }

    private void Inventory_OnActiveItemChangeFromPickup(Item item)
    {
        renderer.sprite = item.spriteLoaded;
        animationType = item.useAnimation;
    }

    private void Inventory_OnActiveItemChangeFromUnequip()
    {
        animationType = Item.UseAnimation.NONE;
    }
}
