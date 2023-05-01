using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseAnimation : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Animator animator;
    public Item.UseAnimation animationType;
    public bool InMotion { get; set; } = false;

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

        InMotion = true;
        renderer.enabled = true;
        animator.SetFloat("velx", lastDirection.x);
        animator.SetFloat("vely", lastDirection.y);
        animator.SetTrigger(animationType.ToString());

        switch (animationType)
        {
            case Item.UseAnimation.NONE:
            case Item.UseAnimation.SLASH:
            case Item.UseAnimation.STAB:
            case Item.UseAnimation.OVERHEAD_SWING:
                break;
            case Item.UseAnimation.DIG:
                Cursor.RemoveGrass();
                break;
        }
    }

    public void EndAnimation()
    {
        renderer.enabled = false;
        InMotion = false;
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
