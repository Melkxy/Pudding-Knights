using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot
{
    public ItemType[] allowedItems = new ItemType[0];

    [System.NonSerialized]
    public InventoryInterface parent;

    [System.NonSerialized]
    public GameObject slotDisplay;
    
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;

    [SerializeField]private Item _item;
    public Item item { get { return _item; } private set { _item = value; } }
    [SerializeField]private int _amount;
    public int amount { get { return _amount; } private set { _amount = value; } }

    public ItemObject itemObject
    {
        get
        {
            if (item.id >= 0)
            {
                return parent.inventory.database.itemObjects[item.id];
            }
            return null;
        }
    }

    public InventorySlot()
    {
        UpdateSlot(new Item(), 0);
    }

    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }

    public void UpdateSlot(Item _item, int _amount)
    {
        if (OnBeforeUpdate != null && !OnBeforeUpdate.Equals(null))
        {
            OnBeforeUpdate.Invoke(this);
        }
        item = _item;
        amount = _amount;
        if (OnAfterUpdate != null && !OnAfterUpdate.Equals(null))
        {
            OnAfterUpdate.Invoke(this);
        }
    }

    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }

    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value);
    }

    public void RemoveAmount(int value)
    {
	if (amount - value <= 0)
	{
	    RemoveItem();
	}
	else
	{
            UpdateSlot(item, amount -= value);
	}
    }

    public bool AllowedToPlace(ItemObject _itemObject)
    {
        if (allowedItems.Length <= 0 || (_itemObject == null || _itemObject.Equals(null)) || _itemObject.data.id < 0)
        {
            return true;
        }
        for (int i = 0; i < allowedItems.Length; i++)
        {
            if (_itemObject.type == allowedItems[i])
            {
                return true;
            }
        }
        return false;
    }
}
