using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnActiveItemChangeFromPickupHandler(string animation);
    public static event OnActiveItemChangeFromPickupHandler OnActiveItemChangeFromPickup;

    public static GameObject itemPrefab;
    public static Transform[] slot;
    public static byte CountEmptySlots = 9;
    public static byte activeSlot = 1;

    private void Awake()
    {
        itemPrefab = Resources.Load("UI/Item") as GameObject;
        slot = GetComponentsInChildren<Transform>(true);
        CountEmptySlots = CountEmptySlot();
    }

    public static void AddItem(ItemMono itemMono)
    {
        byte nextAvailableSlot = GetEmptySlot();

        if (nextAvailableSlot >= slot.Length)
            return;

        ItemMono iMono = Instantiate(itemPrefab, slot[nextAvailableSlot]).GetComponent<ItemMono>();
        CopyClassValues(itemMono, iMono);
        iMono.Setup();

        if (nextAvailableSlot == activeSlot)
            OnActiveItemChangeFromPickup?.Invoke(itemMono.item.animationControllerOverrideFileName);
    }

    public static byte GetEmptySlot()
    {
        for (byte i = 0; i < slot.Length; i++)
        {
            if (slot[i].transform.childCount == 0)
            {
                return i;
            }
        }

        return byte.MaxValue;
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

    public static void CopyClassValues(ItemMono source, ItemMono target)
    {
        FieldInfo[] sourceFields = source.GetType().GetFields(BindingFlags.Public |
                                                         BindingFlags.NonPublic |
                                                         BindingFlags.Instance);
        int i = 0;
        for (i = 0; i < sourceFields.Length; i++)
        {
            var value = sourceFields[i].GetValue(source);
            sourceFields[i].SetValue(target, value);
        }
    }

    
}
