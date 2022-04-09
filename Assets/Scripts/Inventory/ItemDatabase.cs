using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]private ItemObject[] _itemObjects;
    public ItemObject[] itemObjects { get { return _itemObjects; } private set { _itemObjects = value; } }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < itemObjects.Length; i++)
        {
            if (!itemObjects[i].CompareID(i))
            {
                itemObjects[i].CreateItem(itemObjects[i].data.name, i, itemObjects[i].data.price, itemObjects[i].data.buffs,  itemObjects[i].data.recoveries);
            }
        }
    }

    public void OnBeforeSerialize()
    {
    }
}
