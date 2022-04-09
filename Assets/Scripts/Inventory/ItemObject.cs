using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class ItemObject : ScriptableObject
{
    [SerializeField]private Sprite _icon;
    public Sprite icon { get { return _icon; } private set { _icon = value; } }
    [SerializeField]private ItemType _type;
    public ItemType type { get { return _type; } private set { _type = value; } }
    [SerializeField]private int _level = 1;
    public int level { get { return _level; } private set { _level = value; } }
    [TextArea(5,20)]
    [SerializeField]private string description;
    [SerializeField]private bool _stackable;
    public bool stackable { get { return _stackable; } private set { _stackable = value; } }
    [SerializeField]private Item _data = new Item();
    public Item data { get { return _data; } private set { _data = value; } }

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    public Item CreateItem(string _name, int _id, int _price, ItemBuff[] _buffs, ItemRecovery[] _recoveries)
    {
        Item newItem = new Item(_name, _id, _price, _buffs, _recoveries);
	data = newItem;
        return newItem;
    }

    public bool CompareID(int _id)
    {
	bool compare = data.id == _id;
	return data.id == _id;
    }
}
