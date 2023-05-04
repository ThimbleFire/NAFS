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
        EMPTY,
        SLASH,
        STAB,
        OVERHEAD_SWING,
        DIG
    }
    public enum Behaviour
    {
        NONE,
        DAMAGE,
        MINE,
        CHOP_TREE,
        DIG_GRASS,
        TILL_EARTH,
        SOW
    };

    public string Name = string.Empty;
    public Type ItemType = Item.Type.TOOL;
    public UseAnimation useAnimation = Item.UseAnimation.NONE;
    public Behaviour behaviour = Behaviour.NONE;
    public string sprite = string.Empty;

    [NonSerialized]
    public UnityEngine.Sprite spriteLoaded;
}
