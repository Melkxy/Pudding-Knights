using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenuActions : MonoBehaviour
{
    private PauseMenuManager escMenu;
    private SaveManager saveManager;

    private void Awake()
    {
	escMenu = GetComponent<PauseMenuManager>();
	saveManager = FindObjectOfType<SaveManager>();
    }

    public void GoToTittle()
    {
	FindObjectOfType<Player>().ClearInventories();
	escMenu.CloseMenu();
	//saveManager.SaveGame();
	StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
	yield return new WaitForEndOfFrame();
	SceneManager.LoadScene(0);
    }
}
