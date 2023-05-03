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

        item.spriteLoaded = Resources.Load<Sprite>(item.sprite);
        image.sprite = item.spriteLoaded;
    }
}
