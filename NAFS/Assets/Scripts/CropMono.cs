using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
class CropMono : MonoBehaviour
{
    private string cropName;
    private int GrowthStage {get; set;} = 0;
    private int Life {get; set;} = 2;
    private int YieldMin = 3;
    private int YieldMax = 4;
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
        renderer.sprite = sprites[GrowthStage++];
        Life = 2;

        if (GrowthStage == sprites.Length)
            GameTime.OnTck -= GameTime_OnTck;
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("playertoolcollider")) {
            Life -= 1;
            if(Life <= 0) {
                //distribute harvest
            }
        }
    }    
}
