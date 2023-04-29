using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMono : MonoBehaviour
{
    public Item item;
    public Image image = null;

    //private void Awake()
    //{
    //    if(TryGetComponent<pickup>(out pickup p))
    //    {
    //    }
    //}

    public void Setup()
    {
        image = GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>(item.SpriteUIFilename);
    }
}
