using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ShopSlotUpdated(ShopSlot _slot);

[System.Serializable]
public class ShopSlot
{
    [System.NonSerialized]
    public ShopInterface parent;

    [System.NonSerialized]
    public GameObject slotDisplay;
    
    [System.NonSerialized]
    public ShopSlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public ShopSlotUpdated OnBeforeUpdate;

    [SerializeField]private ItemObject _itemObject;
    public ItemObject itemObject { get { return _itemObject; } private set { _itemObject = value; } }
    [SerializeField]private int _amount;
    public int amount { get { return _amount; } private set { _amount = value; } }

    public ShopSlot()
    {
	UpdateSlot(null, 0);
    }

    public ShopSlot(ItemObject _itemObject, int _amount)
    {
        UpdateSlot(_itemObject, _amount);
    }

    public void UpdateSlot(ItemObject _itemObject, int _amount)
    {
        if (OnBeforeUpdate != null && !OnBeforeUpdate.Equals(null))
        {
            OnBeforeUpdate.Invoke(this);
        }
        itemObject = _itemObject;
        amount = _amount;
        if (OnAfterUpdate != null && !OnAfterUpdate.Equals(null))
        {
            OnAfterUpdate.Invoke(this);
        }
    }

    public void RemoveItem()
    {
	UpdateSlot(null, 0);
    }
}
