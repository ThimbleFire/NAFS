using System;
using UnityEngine;

class CropMono : MonoBehaviour

{
    private int GrowthStage {get; set;} = 0;
    private int Life {get; set;} = 2;
    public Sprite[] sprites;
    
    public SpriteRenderer renderer;
    
    public void Awake()
    {
        renderer.sprite = sprites[GrowthStage];
        GameTime.OnTick += GameTime_OnTick;
    }

    public void Setup(string cropName)
    {
        sprites = ResourceRepository[cropName];
    }
    
    private void GameTime_OnTick()
    {
        if(GrowthStage >= sprites.Length)
            return;
            
        GrowthStage++;
        renderer.sprite = sprites[GrowthStage];
        Life = 2;
    }
    
    
}
