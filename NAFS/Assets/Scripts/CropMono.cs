using System;
using UnityEngine;

class Seed
{
    public enum ID
    {
        Strawberry
    }
}

[RequireComponent(typeof(SpriteRenderer))]
class CropMono : MonoBehaviour
{
    private int GrowthStage {get; set;} = 0;
    private int Life {get; set;} = 2;
    public Sprite[] sprites;
    public SpriteRenderer renderer;
    
    public void Awake()
    {
        renderer.sprite = sprites[GrowthStage];
        GameTime.OnTck += GameTime_OnTck;
    }

    private void GameTime_OnTck()
    {
        if (GrowthStage >= sprites.Length)
            return;

        renderer.sprite = sprites[GrowthStage++];
        Life = 2;
    }   
    
}
