using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuManager : MonoBehaviour
{
    [SerializeField]protected GameObject[] menus;
    [SerializeField]protected KeyCode openKey, closeKey;
    protected bool isOppened;
    [SerializeField]private bool priority;

    protected void Update()
    {
	if (priority)
	{
	    OpenCloseMenuByKey();
	}
    }

    protected void LateUpdate()
    {
	if (!priority)
	{
	    OpenCloseMenuByKey();
	}
    }

    protected void OpenCloseMenuByKey()
    {
	if (Input.GetKeyDown(openKey))
	{
	    if (!isOppened && !GameState.IsBusy())
	    {
		OpenMenu();
	    }
	    else if (isOppened)
	    {
		CloseMenu();
	    }
	}
	else if (Input.GetKeyDown(closeKey))
	{
	    if (isOppened)
	    {
		CloseMenu();
	    }
	}
    }

    public abstract void CloseMenu();

    public abstract void OpenMenu();
}
