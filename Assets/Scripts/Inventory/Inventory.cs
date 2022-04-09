using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField]private ItemType[] _allowedItems;
    public ItemType[] allowedItems { get { return _allowedItems; } private set { _allowedItems = value; } }
    [SerializeField]private InventorySlot[] _slots = new InventorySlot[1];
    public InventorySlot[] slots { get { return _slots; } private set { _slots  = value; } }

    public void Clear()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
        }
    }

    public void SetSlotsAllowedItems()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].allowedItems = allowedItems;
        }
    }

    public Inventory(int _space)
    {
        slots = new InventorySlot[_space];
    }
}
