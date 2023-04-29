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

    public string Name = string.Empty;
    public Type ItemType = Item.Type.TOOL;
    public string SpriteUIFilename = string.Empty;
    public string animationControllerOverrideFileName = string.Empty;
}