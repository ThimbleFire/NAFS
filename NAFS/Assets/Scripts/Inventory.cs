using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnActiveItemChangeFromPickupHandler(Item item);
    public static event OnActiveItemChangeFromPickupHandler OnActiveItemChangeFromPickup;

    public delegate void OnActiveItemChangeUnequipHandler();
    public static event OnActiveItemChangeUnequipHandler OnActiveItemChangeFromUnequip;

    public static Transform[] slot;
    public static byte CountEmptySlots = 9;
    public static int activeSlot = 1;

    private void Awake()
    {
        slot = GetComponentsInChildren<Transform>(true);
        CountEmptySlots = CountEmptySlot();
    }

    public static void AddItem(ItemMono itemMono)
    {
        byte nextAvailableSlot = GetEmptySlot();

        if (nextAvailableSlot >= slot.Length)
            return;

        ItemMono iMono = Instantiate(ResourceRepository.prefab["Item"], slot[nextAvailableSlot]).GetComponent<ItemMono>();
        CopyClassValues(itemMono, iMono);
        iMono.Setup();

        if (nextAvailableSlot == activeSlot)
            OnActiveItemChangeFromPickup?.Invoke(itemMono.item);
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
        for (int i = 0; i < sourceFields.Length; i++) {

            var value = sourceFields[i].GetValue(source);
            sourceFields[i].SetValue(target, value);
        }
    }

    public void Select(int index)
    {
        if (activeSlot == index)
            return;

        activeSlot = index;

        if (slot[index].childCount == 0) {
            OnActiveItemChangeFromUnequip?.Invoke();
            return;
        }

        if (slot[index].GetChild(0).TryGetComponent<ItemMono>(out ItemMono itemMono)) {
            OnActiveItemChangeFromPickup?.Invoke(itemMono.item);
        }
    }
}
