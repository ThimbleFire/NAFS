using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseAnimation : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Animator animator;

    public Item.UseAnimation AnimationType { get; set; } = Item.UseAnimation.NONE;
    public Item.Behaviour ItemBehaviour { get; set; } = Item.Behaviour.NONE;
    public bool InMotion { get; set; } = false;

    public void Awake()
    {
        Inventory.OnActiveItemChangeFromPickup += Inventory_OnActiveItemChangeFromPickup;
        Inventory.OnActiveItemChangeFromUnequip += Inventory_OnActiveItemChangeFromUnequip;

        renderer.enabled = false;
    }
    public Item.Behaviour StartAnimation(Vector3 lastDirection)
    {
        if (AnimationType == Item.UseAnimation.NONE)
            return ItemBehaviour;

        InMotion = true;
        renderer.enabled = true;
        animator.SetFloat("velx", lastDirection.x);
        animator.SetFloat("vely", lastDirection.y);
        animator.SetTrigger(AnimationType.ToString());

        return ItemBehaviour;
    }

    public void EndAnimation()
    {
        renderer.enabled = false;
        InMotion = false;
    }

    private void Inventory_OnActiveItemChangeFromPickup(Item item)
    {
        renderer.sprite = item.spriteLoaded;
        AnimationType = item.useAnimation;
        ItemBehaviour = item.behaviour;
        gameObject.name = item.Name;
    }

    private void Inventory_OnActiveItemChangeFromUnequip()
    {
        renderer.sprite = null;
        AnimationType = Item.UseAnimation.NONE;
        ItemBehaviour = Item.Behaviour.NONE;
        gameObject.name = "tool animation";
    }
}
