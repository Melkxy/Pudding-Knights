using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField]private string savePath;
    [SerializeField]private InventoryType _inventoryType;
    public InventoryType inventoryType { get { return _inventoryType; } private set { _inventoryType = value; } }
    [SerializeField]private ItemDatabase _database;
    public ItemDatabase database { get { return _database; } private set { _database = value; } }
    [SerializeField]private Inventory _Container;
    public Inventory Container { get { return _Container; } private set { _Container = value; } }
    public InventorySlot[] GetSlots { get { return Container.slots; } }

    public bool AddItem(Item _item, int _amount)
    {
	if (EmptySlotCount <= 0)
        {
            return false;
        }
        InventorySlot slot = FindItemOnInventory(_item);
        if (!database.itemObjects[_item.id].stackable || (slot == null || slot.Equals(null)))
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.id <= -1)
                {
                    counter++;
                }
            }
            return counter;
        }
    }

    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.id == _item.id)
            {
                return GetSlots[i];
            }
        }
        return null;
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.id <= -1)
            {
                GetSlots[i].UpdateSlot(_item, _amount);
                return GetSlots[i];
            }
        }
        return null;
    }

    public bool SwapItems(InventorySlot item1, InventorySlot item2)
    {
	if (item1 == item2 || item1.amount <= 0 || item2 == null)
	{
	    return false;
	}
        if (item1.item.id == item2.item.id)
        {
	    if (database.itemObjects[item1.item.id].stackable)
	    {
            	item2.AddAmount(item1.amount);
            	item1.RemoveItem();
		return true;
  	    }
	    else
	    {
		InventorySlot temp = new InventorySlot(item2.item, item2.amount);
                item2.UpdateSlot(item1.item, item1.amount);
                item1.UpdateSlot(temp.item, temp.amount);
	        return true;
	    }
        }
        else if (item2.AllowedToPlace(item1.itemObject) && item1.AllowedToPlace(item2.itemObject))
        {
	    if (item2.parent.inventory.inventoryType == InventoryType.Equipment)
	    {
		if (item1.itemObject.level > FindObjectOfType<PlayerStats>().level)
		{
		    return false;
		}
	    }
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
	    return true;
        }
        return false;
    }

    public void UseItem(InventorySlot item)
    {
	if (item.itemObject.type != ItemType.Consumable)
	{
	    Debug.LogWarning("INVALID ITEM TO USE");
	    return;
	}
	PlayerStats player = FindObjectOfType<PlayerStats>();
	foreach (ItemRecovery recovery in item.item.recoveries)
	{
	    if (recovery.type == ItemRecoveryType.Health)
	    {
		player.RecoverHP(recovery.recovery);
	    }
	    else if (recovery.type == ItemRecoveryType.Mana)
	    {
		player.RecoverMP(recovery.recovery);
	    }
	}
	item.RemoveAmount(1);
    }

    [ContextMenu("Save")]
    public void  Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(newContainer.slots[i].item, newContainer.slots[i].amount);
            }
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }

    [ContextMenu("Reset Allowed Items")]
    public void ResetAllowedItems()
    {
        Container.SetSlotsAllowedItems();
    }
}
