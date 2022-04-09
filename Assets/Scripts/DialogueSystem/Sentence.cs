using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence
{
    [TextArea(3, 10)]
    [SerializeField]
    private string _sentenceText;
    public string sentenceText { get { return _sentenceText; } private set { _sentenceText = value; } }

    public Sentence(string text)
    {
	sentenceText = text;
    }
}
