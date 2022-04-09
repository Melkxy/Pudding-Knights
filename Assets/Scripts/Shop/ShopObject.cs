using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "Shop System/Shop")]
public class ShopObject : ScriptableObject
{
    [SerializeField]private string _shopName;
    public string shopName { get { return _shopName; } private set { _shopName = value; } }
    [SerializeField]private ItemDatabase _database;
    public ItemDatabase database { get { return _database; } private set { _database = value; } }
    [SerializeField]private Shop Container;
    public ShopSlot[] GetSlots { get { return Container.slots; } }
}
