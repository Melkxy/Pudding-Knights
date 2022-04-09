using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField]private string _name;
    public string name { get { return _name; } private set { _name = value; } }

    [SerializeField]private int _id = -1;
    public int id { get { return _id; } private set { _id = value; } }

    [SerializeField]private int _price;
    public int price { get { return _price; } private set { _price = value; } }

    [SerializeField]private ItemBuff[] _buffs;
    public ItemBuff[] buffs { get { return _buffs; } private set { _buffs = value; } }

    [SerializeField]private ItemRecovery[] _recoveries;
    public ItemRecovery[] recoveries { get { return _recoveries; } private set { _recoveries = value; } }
    
    public Item()
    {
		name = "";
		id = -1;
		price = 0;
		buffs = new ItemBuff[0];
		recoveries = new ItemRecovery[0];
    }

    public Item(string _itemName, int _itemId, int _price, ItemBuff[] _buffs, ItemRecovery[] _recoveries)
    {
		name = _itemName;
		id = _itemId;
		price = _price;
		buffs = _buffs;
		recoveries = _recoveries;
    }

    public Item(ItemObject item)
    {
        name = item.data.name;
        id = item.data.id;
		price = item.data.price;
		buffs = item.data.buffs;
		recoveries = item.data.recoveries;
    }

    public int GetItemBuff(AttributeType type)
    {
	foreach (ItemBuff buff in buffs)
	{
	    if (buff.attribute == type)
	    {
		return buff.value;
	    }
	}
	Debug.Log("That item don't contains this buff type!");
	return 0;
    }

    public int GetItemRecovery(ItemRecoveryType type)
    {
	foreach (ItemRecovery recovery in recoveries)
	{
	    if (recovery.type == type)
	    {
		return recovery.recovery;
	    }
	}
	Debug.Log("That item don't contains this buff type!");
	return 0;
    }
}
