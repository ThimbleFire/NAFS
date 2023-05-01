using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("Item")]
public class Item
{
    public enum Type
    {
        TOOL,
        FOOD,
        SEED
    }

    public enum UseAnimation
    {
        NONE,
        SLASH,
        STAB,
        OVERHEAD_SWING
    }

    public string Name = string.Empty;
    public Type ItemType = Item.Type.TOOL;
    public UseAnimation animation = Item.UseAnimation.NONE;
    public string sprite = string.Empty;
    [NonSerializable]
    public Sprite spriteLoaded;
}
