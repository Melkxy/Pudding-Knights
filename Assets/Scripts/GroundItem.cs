using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    [SerializeField]private GroundItemObject item;
    private Player player;

    private void Awake()
    {
	player = FindObjectOfType<Player>();
    }

    public void SetItemDrop(GroundItemObject groundItem)
    {
	item = new GroundItemObject(groundItem.item, groundItem.amount);
    }

    public void PickItem()
    {
	    foreach (InventoryObject inventory in player.inventories)
	    {
		foreach (ItemType type in inventory.Container.allowedItems)
		{
		    if (item.item.type == type)
		    {
			StartCoroutine(ItemPicked(inventory));
			FindObjectOfType<ItemRewardMessages>().ShowLevelUp(item.item, item.amount);
		    }
		}
	    }
    }

    private IEnumerator ItemPicked(InventoryObject inventory)
    {
	bool added = inventory.AddItem(new Item(item.item), item.amount);
	if (added)
	{
	    yield return new WaitForEndOfFrame();
	    Destroy(gameObject);
	}
    }
}

[System.Serializable]
public class GroundItemObject
{
    [SerializeField]private ItemObject _item;
    public ItemObject item { get { return _item; } private set { _item = value; } }
    [SerializeField]private int _amount;
    public int amount { get { return _amount; } private set { _amount = value; } }

    public GroundItemObject(ItemObject item_, int amount_)
    {
	item = item_;
	amount = amount_;
    }
}