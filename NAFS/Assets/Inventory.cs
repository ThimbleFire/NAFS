using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static GameObject itemPrefab;
    public static Transform[] slot;
    public static byte CountEmptySlots = 9;

    private void Awake()
    {
        itemPrefab = Resources.Load("UI/Item") as GameObject;
        slot = GetComponentsInChildren<Transform>(true);
        CountEmptySlots = CountEmptySlot();
    }

    public static void AddItem(Item item)
    {
        Instantiate(itemPrefab, GetEmptySlot());
    }

    public static Transform GetEmptySlot()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].transform.childCount == 0)
                return slot[i];
        }

        return null;
    }

    public static byte CountEmptySlot()
    {
        byte count = 0;

        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].transform.childCount == 0)
                count++;
        }

        return count;
    }
}
