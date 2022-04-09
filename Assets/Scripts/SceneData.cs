using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneData
{
    //[SerializeField]private SavableItem[] _savableItems;
    //public SavableItem[] savableItems { get { return _savableItems; } private set { _savableItems = value; } }
    [SerializeField]private int _sceneIndex;
    public int sceneIndex { get { return _sceneIndex; } private set { _sceneIndex = value; } }

    public SceneData(int sceneIndex_)
    {
	//savableItems = savableItems_;
	sceneIndex = sceneIndex_;
    }
}