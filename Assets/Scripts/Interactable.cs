using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private KeyCode interactKey;
    [SerializeField]
    private bool canInteract;
    [SerializeField]
    private UnityEvent action;

    private bool isInRange;

    private void Awake()
    {
	canInteract = true;
    }

    private void Update()
    {
	if (isInRange && canInteract && !GameState.IsBusy())
	{
	    if (Input.GetKeyDown(interactKey))
	    {
		action.Invoke();
	    }
	}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	if(other.gameObject.CompareTag("Player"))
	{
	    isInRange = true;
	    other.GetComponent<Player>().hint.SetActive(true);
	}
    }

    private void OnTriggerStay2D(Collider2D other)
    {
	if(other.gameObject.CompareTag("Player"))
	{
	    isInRange = true;
	    other.GetComponent<Player>().hint.SetActive(true);
	}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
	if(other.gameObject.CompareTag("Player"))
	{
	    isInRange = false;
	    other.GetComponent<Player>().hint.SetActive(false);
	}
    }
}
