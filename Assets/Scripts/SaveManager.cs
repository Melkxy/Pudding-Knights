using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SaveGame()
    {
	Player player = FindObjectOfType<Player>();
	player.SavePlayer();
	player.equipment.Save();
	foreach (InventoryObject inventory in player.inventories)
	{
	    inventory.Save();
	}
	SaveSystem.SaveScene();
    }

    public void LoadGame()
    {
	Player player;
	player = FindObjectOfType<Player>();
	player.LoadPlayer();
	player.equipment.Load();
	player.UpdateEquipmentSlots();
	foreach (InventoryObject inventory in player.inventories)
	{
	    inventory.Load();
	}
	SceneData data = SaveSystem.LoadScene();

	//SavableItem[] savableItems = FindObjectsOfType<SavableItem>();

	//for (int i = 0; i < data.savableItems.Length; i++)
	//{
	    //for (int j = 0; j < savableItems.Length; j++)
	    //{
		//if (data.savableItems[i] == savableItems[j])
		//{
		    //j = savableItems.Length;
		//}
		//else if (j == savableItems.Length + 1)
		//{
		    //Destroy(savableItems[j].gameObject);
		//}
	    //}
	//}
    }
}
