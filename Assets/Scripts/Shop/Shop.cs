using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shop
{
    [SerializeField]private ShopSlot[] _slots = new ShopSlot[1];
    public ShopSlot[] slots { get { return _slots; } private set { _slots  = value; } }

    public void Clear()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
        }
    }

    public Shop(int _space)
    {
        slots = new ShopSlot[_space];
    }
}
