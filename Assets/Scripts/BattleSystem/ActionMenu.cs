using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
    [SerializeField]private Button[] actions;

    public void SetActions(bool b)
    {
	foreach (Button btn in actions)
	{
	    btn.interactable = b;
	}
    }
}
