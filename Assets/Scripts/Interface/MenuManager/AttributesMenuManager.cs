using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesMenuManager : MenuManager
{
    [SerializeField]private BookMark attributesBookMark;

    private AttributesInterfaceComponents components;

    private void Awake()
    {
	components = GetComponent<AttributesInterfaceComponents>();
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
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(true);
	}
	components.UpdateComponents();
	attributesBookMark.SetMark(true);
    }
}
