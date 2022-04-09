using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue;
    private DialogueManager dialogueManager;

    private void Awake()
    {
	if (dialogueManager == null)
	{
	    dialogueManager = FindObjectOfType<DialogueManager>();
	}
    }

    public DialogueTrigger(Dialogue _dialogue, DialogueManager _dialogueManager)
    {
	dialogue = _dialogue;
	dialogueManager = _dialogueManager;
    }

    public void TriggerDialogue()
    {
	StartCoroutine(dialogueManager.StartDialogue(dialogue));
    }
}
