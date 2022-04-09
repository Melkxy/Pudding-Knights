using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMark : MonoBehaviour
{
    [SerializeField]private GameObject[] menusToOpen, menusToClose;
    [SerializeField]private BookMark[] otherMarks;
    private bool _triggered;
    public bool triggered { get { return _triggered; } private set { _triggered = value; } }
    private Animator anim;

    private void Awake()
    {
	anim = GetComponent<Animator>();
    }

    public void TriggerMenus()
    {
	SetMark(true);
	foreach (BookMark mark in otherMarks)
	{
	    mark.SetMark(false);
	}
	foreach (GameObject menu in menusToOpen)
	{
	    menu.SetActive(true);
	}
	foreach (GameObject menu in menusToClose)
	{
	    menu.SetActive(false);
	}
    }

    public void SetMark(bool down)
    {
	anim.SetBool("isOpen", down);
	triggered = down;
    }
}
