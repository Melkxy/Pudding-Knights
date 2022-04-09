using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuManager : MenuManager
{
    [SerializeField]private ShopInterface shopInterface;
    private ShopInterfaceComponents components;

    private void Awake()
    {
	components = GetComponent<ShopInterfaceComponents>();
    }

    public override void CloseMenu()
    {
	isOppened = false;
	GameState.Busy(false);
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(false);
	}
    }

    public override void OpenMenu()
    {
	isOppened = true;
	GameState.Busy(true);
	if (shopInterface.details != null)
	    shopInterface.details.Reset();
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(true);
	}
	components.UpdateComponents(shopInterface);
    }

    public void OpenMenuByShop(ShopObject shop)
    {
	isOppened = true;
	GameState.Busy(true);
	shopInterface.OpenShop(shop);
	if (shopInterface.details != null)
	    shopInterface.details.Reset();
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(true);
	}
	components.UpdateComponents(shopInterface);
    }
}
