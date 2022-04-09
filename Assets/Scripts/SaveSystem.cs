using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
	BinaryFormatter formatter = new BinaryFormatter();
	string path = SetPlayerPath();
	FileStream stream = new FileStream(path, FileMode.Create);

	PlayerData data = new PlayerData(player);

	formatter.Serialize(stream, data);
	stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
	string path = SetPlayerPath();
	if (File.Exists(path))
	{
	    BinaryFormatter formatter = new BinaryFormatter();
	    FileStream stream = new FileStream(path, FileMode.Open);
	    PlayerData data = formatter.Deserialize(stream) as PlayerData;
	    stream.Close();

	    return data;
	}
	else
	{
	    Debug.LogError("Player save file not found in " + path);
	    return null;
	}
    }

    private static string SetPlayerPath()
    {
	return Application.persistentDataPath + "/player.save";
    }

    public static void SaveScene()
    {
	BinaryFormatter formatter = new BinaryFormatter();
	string path = SetScenePath();
	FileStream stream = new FileStream(path, FileMode.Create);

	//SavableItem[] items = GameObject.FindObjectsOfType<SavableItem>();

	//SceneData data = new SceneData(items, SceneManager.GetActiveScene().buildIndex);

	SceneData data = new SceneData(SceneManager.GetActiveScene().buildIndex);

	formatter.Serialize(stream, data);
	stream.Close();
    }

    public static SceneData LoadScene()
    {
	string path = SetScenePath();
	if (File.Exists(path))
	{
	    BinaryFormatter formatter = new BinaryFormatter();
	    FileStream stream = new FileStream(path, FileMode.Open);
	    SceneData data = formatter.Deserialize(stream) as SceneData;
	    stream.Close();

	    return data;
	}
	else
	{
	    Debug.LogError("Player save file not found in " + path);
	    return null;
	}
    }

    private static string SetScenePath()
    {
	return Application.persistentDataPath + "/scene.save";
    }
}
