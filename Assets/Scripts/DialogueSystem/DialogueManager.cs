using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private GameObject tittlePanel;
    [SerializeField]
    private TMP_Text tittleText;
    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField]
    private Animator dialogueAnim;

    [SerializeField]
    private KeyCode nextKey;

    private bool canNext;

    private void Awake()
    {
        sentences = new Queue<string>();
	tittleText.text = "";
	dialogueText.text = "";

	dialogueCanvas.SetActive(false);
    }

    void LateUpdate()
    {
	if (Input.GetKeyDown(nextKey) && canNext)
	{
	    DisplayNextSentence();
	}
    }

    public IEnumerator StartDialogue(Dialogue dialogue)
    {
	canNext = false;
	GameState.Busy(true);

	dialogueCanvas.SetActive(true);
	
	dialogueAnim.SetBool("isOpen", true);

	if (dialogue.tittle == "")
	{
	    tittlePanel.SetActive(false);
	}
	else
	{
	    tittlePanel.SetActive(true);
	    tittleText.text = dialogue.tittle;
	}

	sentences.Clear();

	foreach (Sentence sentence in dialogue.sentences)
	{
	    sentences.Enqueue(sentence.sentenceText);
	}

	yield return new WaitForEndOfFrame();
	DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
	if (sentences.Count == 0)
	{
	    EndDialogue();
	}
	else
	{
	    string sentence = sentences.Dequeue();
	    StopAllCoroutines();
	    StartCoroutine(TypeSentence(sentence));
	    canNext = true;
	}
    }

    IEnumerator TypeSentence(string sentence)
    {
	dialogueText.text = "";
	for (int i = 0; i < sentence.Length; i++)
	{
	    dialogueText.text += sentence[i];
	    yield return 2f;
	}
    }

    void EndDialogue()
    {
	dialogueAnim.SetBool("isOpen", false);
	sentences = new Queue<string>();

	GameState.Busy(false);
    }
}
