using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMenuManager : MenuManager
{
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
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(true);
	}
    }
}
