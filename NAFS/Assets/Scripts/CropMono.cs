using System;
using UnityEngine;

class CropMono : MonoBehaviour

{
    private int GrowthStage {get; set;} = 0;
    public Sprite[] sprites;
    
    public SpriteRenderer renderer;
    
    public void Awake()
    {
        renderer.sprite = sprites[GrowthStage];
        GameTime.OnTick += GameTime_OnTick;
    }

  
    
    private void GameTime_OnTick()
    {
        if(GrowthStage >= GrowthStageMax)
            return;
            
        GrowthStage++;
        renderer.sprite = sprites[GrowthStage];

    }
    
    
}
