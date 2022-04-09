using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    [SerializeField]private int firstSceneIndex;
    private int lastSceneIndex;

    private void Awake()
    {
	DontDestroyOnLoad(gameObject);
    }

    public bool VerifySaveFile()
    {
	string path = Application.persistentDataPath + "/player.save";
	return File.Exists(path);
    }

    public void StartGame()
    {
	StartCoroutine(StartLoader());
    }

    private IEnumerator StartLoader()
    {
	//if (!VerifySaveFile())
	//{
	    //AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(firstSceneIndex, LoadSceneMode.Single);

	    //while (!asyncLoadScene.isDone)
	    //{
	    	//yield return null;
	    //}

	    //yield return new WaitForEndOfFrame();
	
	    //SaveManager saveManager = FindObjectOfType<SaveManager>();

	    //saveManager.SaveGame();
	//}
	//else
	//{
	    SceneManager.LoadScene(firstSceneIndex);
	    yield return 0;
	//}
    }

    public void ContineGame()
    {
	if (!VerifySaveFile())
	    return;
	
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

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
