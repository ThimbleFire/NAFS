using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
class CropMono : MonoBehaviour
{
    private string CropName { get; set; }
    private int GrowthStage { get; set; } = 0;
    private int Life { get; set; } = 2;
    private int YieldMin { get; set; } = 3;
    private int YieldMax { get; set; } = 4;
    private Sprite[] sprites;

    private bool SubscribedToClock { get; set; } = false;
    private bool CanBeHit { get; set; } = true;

    public SpriteRenderer renderer;

    public void Setup(string cropName)
    {
        sprites = ResourceRepository.sprites[cropName];
        renderer.sprite = sprites[GrowthStage];
        GameTime.OnTck += GameTime_OnTck;
        SubscribedToClock = true;
        CropName = cropName;
    }

    private void GameTime_OnTck()
    {
        renderer.sprite = sprites[GrowthStage++];
        Life = 2;

        if (GrowthStage == sprites.Length)
        {
            GameTime.OnTck -= GameTime_OnTck;
            SubscribedToClock = false;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (ItemUseAnimation.InMotion == false)
            return;

        if (CanBeHit == false)
            return;

        CanBeHit = false;

        if (ItemUseAnimation.ItemBehaviour != Item.Behaviour.DAMAGE)
            return;

        if (collision.gameObject.CompareTag("playertoolcollider") == false)
            return;


        if (--Life <= 0)
        {
            if (SubscribedToClock)
                GameTime.OnTck -= GameTime_OnTck;

            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CanBeHit = true;
    }
}
