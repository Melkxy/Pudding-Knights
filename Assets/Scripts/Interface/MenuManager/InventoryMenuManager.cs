using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuManager : MenuManager
{
    [SerializeField]private BookMark currentBookMark;
    [SerializeField]private BookMark[] allBookMarks;
    [SerializeField]private GameObject shopBookMark;
    [SerializeField]private GameObject attributesBookMark;
    [SerializeField]private ItemDetailsInterface details;

    private InventoryInterfaceComponents components;

    private void Awake()
    {
	components = GetComponent<InventoryInterfaceComponents>();
    }

    public override void CloseMenu()
    {
	foreach (BookMark mark in allBookMarks)
	{
	    if (mark.triggered)
	    {
		currentBookMark = mark;
		break;
	    }
	}
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
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(true);
	}
	details.Reset();
	currentBookMark.TriggerMenus();
	attributesBookMark.SetActive(true);
	shopBookMark.SetActive(false);
	components.UpdateComponents();
    }

    public void OpenMenuFromShop()
    {
	isOppened = true;
	GameState.Busy(true);
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(true);
	}
	details.Reset();
	currentBookMark.TriggerMenus();
	attributesBookMark.SetActive(false);
	shopBookMark.SetActive(true);
	components.UpdateComponents();
    }
}
