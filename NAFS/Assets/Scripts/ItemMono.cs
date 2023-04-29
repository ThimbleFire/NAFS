using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMono : MonoBehaviour
{
    public Item item;
    public Image image = null;

    public void Setup()
    {
        image = GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>(item.SpriteUIFilename);
    }
}
