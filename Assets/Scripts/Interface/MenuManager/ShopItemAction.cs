using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemAction : MonoBehaviour
{
    private ShopItemDetailsInterface shopDetails;
    private PlayerMoney playerMoney;
    private Player player;

    private void Awake()
    {
	shopDetails = GetComponent<ShopItemDetailsInterface>();
	playerMoney = FindObjectOfType<PlayerMoney>();
	player = FindObjectOfType<Player>();
    }

    public void BuyItem()
    {
	if (playerMoney.HaveEnoughMoney(shopDetails.slot.itemObject.data.price * shopDetails.currentPurchaseAmount))
	{
	    foreach (InventoryObject inventory in player.inventories)
	    {
		foreach (ItemType type in inventory.Container.allowedItems)
		{
		    if (shopDetails.slot.itemObject.type == type)
		    {
			bool added = inventory.AddItem(shopDetails.slot.itemObject.data, shopDetails.currentPurchaseAmount);
			if (added)
			{
			    playerMoney.Pay(shopDetails.slot.itemObject.data.price * shopDetails.currentPurchaseAmount);
			    shopDetails.ShowUp(shopDetails.slot);
			    return;
			}
		    }
		}
	    }
	}
    }
}
