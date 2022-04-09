using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MenuManager
{
    //[SerializeField]private GameObject continueBTN;

    private MainMenu mainMenu;

    private void Awake()
    {
	mainMenu = FindObjectOfType<MainMenu>();
    }

    public override void CloseMenu()
    {
	isOppened = false;
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(false);
	}
    }

    public override void OpenMenu()
    {
	isOppened = true;
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(true);
	}
	//continueBTN.SetActive(mainMenu.VerifySaveFile());
    }
}
