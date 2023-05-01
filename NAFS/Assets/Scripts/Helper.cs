using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static bool IsNullOrDefault<T>(T value)
    {
        return Equals(value, default(T));
    }

    public const string hexMagic = "<color=#4850B8>";
    public const string hexRare = "<color=#FFFF00>";
    public const string hexGray = "<color=#8A8A8A>";
    public const string hexRed = "<color=#FF0000>";
    public const string hexUnique = "<color=#908858>";
    public const string hexWhite = "<color=#FFFFFF>";
    public const string hexEnd = "</color>";

    public static string[] ItemTypeNames = new string[3]
    {
            "Tool",
            "Food",
            "Seed",
    };

    public static string[] ItemAnimationNames = new string[4]
    {
            "None",
            "Slash",
            "Stab",
            "Overhead Swing"
    };
}
