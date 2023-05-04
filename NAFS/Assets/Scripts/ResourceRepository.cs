using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRepository : MonoBehaviour
{
    public static Dictionary<string, Sprite[]> sprites = new Dictionary<string, Sprite[]>();
    public static Dictionary<string, GameObject> prefab = new Dictionary<string, GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(this);

        sprites.Add("StrawberrySeeds", Resources.LoadAll<Sprite>("Environment/Seeds/strawberries"));

        prefab.Add("Item", Resources.Load("UI/Item") as GameObject);
        prefab.Add("Seed", Resources.Load("Items/Seed") as GameObject);
    }
}
