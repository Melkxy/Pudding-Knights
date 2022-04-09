using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuManager : MenuManager
{
    private float _timeScale;

    public override void CloseMenu()
    {
	isOppened = false;
	GameState.Busy(false);
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(false);
	}
	if (_timeScale > 0)
	    Time.timeScale = _timeScale;
    }

    public override void OpenMenu()
    {
	isOppened = true;
	GameState.Busy(true);
	foreach(GameObject menu in menus)
	{
	   menu.SetActive(true);
	}
	_timeScale = Time.timeScale;
	Time.timeScale = 0;
    }
}
