using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameOver : MonoBehaviour
{
    private GameOverMenuManager menu;

    private int lastSceneIndex;

    private void Awake()
    {
	menu = FindObjectOfType<GameOverMenuManager>();
    }

    public void ContinueFromSave()
    {
	StartCoroutine(ContinueLoader());
    }

    private IEnumerator ContinueLoader()
    {
	string path = Application.persistentDataPath + "/scene.save";
	if (File.Exists(path))
	{
	    BinaryFormatter formatter = new BinaryFormatter();
	    FileStream stream = new FileStream(path, FileMode.Open);
	    SceneData data = formatter.Deserialize(stream) as SceneData;
	    stream.Close();

	    lastSceneIndex = data.sceneIndex;
	}
	else
	{
	    Debug.LogError("Player save file not found in " + path);
	}

	AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(lastSceneIndex, LoadSceneMode.Single);

	while (!asyncLoadScene.isDone)
	{
	    yield return null;
	}

	yield return new WaitForEndOfFrame();
	
	SaveManager saveManager = FindObjectOfType<SaveManager>();

	saveManager.LoadGame();
    }

    public void BackToTitle()
    {
	FindObjectOfType<Player>().ClearInventories();
	menu.CloseMenu();
	StartCoroutine(LoadMainMenu());
    }

	public void Respown(){
		FindObjectOfType<Player>().ClearInventories();
		menu.CloseMenu();
		SceneManager.LoadScene(Application.loadedLevel);
	}

    private IEnumerator LoadMainMenu()
    {
	yield return new WaitForEndOfFrame();
	SceneManager.LoadScene(0);
    }
}
