using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
class CropMono : MonoBehaviour
{
    private string cropName;
    private int GrowthStage {get; set;} = 0;
    private int Life {get; set;} = 2;
    public Sprite[] sprites;
    public SpriteRenderer renderer;
    
    public void Setup(string cropName)
    {
        sprites = ResourceRepository.sprites[cropName];
        renderer.sprite = sprites[GrowthStage];
        GameTime.OnTck += GameTime_OnTck;
        this.cropName = cropName;
    }

    private void GameTime_OnTck()
    {
        if (GrowthStage >= sprites.Length)
            return;

        renderer.sprite = sprites[GrowthStage++];
        Life = 2;
    }   
    
}
