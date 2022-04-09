using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField]
    private string _tittle;
    public string tittle { get { return _tittle; } private set { _tittle = value; } }
    [SerializeField]
    private List<Sentence> _sentences = new List<Sentence>();
    public List<Sentence> sentences { get { return _sentences; } private set { _sentences = value; } }

    public Dialogue()
    {
	tittle = "";
    }

    public Dialogue(string _tittle)
    {
	tittle = _tittle;
    }

    public void AddSentence(Sentence _sentence)
    {
	sentences.Add(_sentence);
    }
}
