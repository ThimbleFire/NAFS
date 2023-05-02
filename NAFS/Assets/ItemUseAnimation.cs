using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseAnimation : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Animator animator;
    public Item.UseAnimation animationType;
    public Item.Behaviour itemBehaviour;
    public bool InMotion { get; set; } = false;

    public void Awake()
    {
        Inventory.OnActiveItemChangeFromPickup += Inventory_OnActiveItemChangeFromPickup;
        Inventory.OnActiveItemChangeFromUnequip += Inventory_OnActiveItemChangeFromUnequip;

        renderer.enabled = false;
    }
    public Item.Behaviour StartAnimation(Vector3 lastDirection)
    {
        if (animationType == Item.UseAnimation.NONE)
            return Item.Behaviour.NONE;

        InMotion = true;
        renderer.enabled = true;
        animator.SetFloat("velx", lastDirection.x);
        animator.SetFloat("vely", lastDirection.y);
        animator.SetTrigger(animationType.ToString());

        return itemBehaviour;
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
        itemBehaviour = item.behaviour;
    }

    private void Inventory_OnActiveItemChangeFromUnequip()
    {
        animationType = Item.UseAnimation.NONE;
    }
}
