using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsInterface : MonoBehaviour
{
    [SerializeField]private GameObject itemNameBox;
    [SerializeField]private TMP_Text itemName;
    [SerializeField]private GameObject itemLevelBox;
    [SerializeField]private TMP_Text itemLevel;
    [SerializeField]private Color availableLevelColor, unavailableLevelColor;
    [SerializeField]private ItemStatus weaponStatus, armorStatus, consumableStatus;

    [SerializeField]private Button useBTN, equipBTN, unequipBTN;

    [SerializeField]private ItemType[] equipableItems, consumableItems;

    private bool _clicked;
    public bool clicked { get { return _clicked; } private set { _clicked = value; } }

    public void ShowUp(InventorySlot _slot)
    {
	if (_slot.item.id < 0)
	{
	    return;
	}
	itemNameBox.SetActive(true);
	itemName.text = _slot.item.name;

	foreach (ItemType type in equipableItems)
	{
	    if (_slot.itemObject.type == type)
	    {
		itemLevelBox.SetActive(true);
		itemLevel.text = "Nv " + _slot.itemObject.level.ToString();
		if ( _slot.itemObject.level > FindObjectOfType<PlayerStats>().level)
		{
		    itemLevel.color = unavailableLevelColor;
		}
		else
		{
		    itemLevel.color = availableLevelColor;
		}
		if (_slot.parent.inventory.inventoryType == InventoryType.Equipment)
	    	{
	            equipBTN.gameObject.SetActive(false);
		    unequipBTN.gameObject.SetActive(true);
	    	}
	    	else
	    	{
	            equipBTN.gameObject.SetActive(true);
		    unequipBTN.gameObject.SetActive(false);
		    if (_slot.itemObject.level <= FindObjectOfType<PlayerStats>().level)
		    {
	            	equipBTN.interactable = true;
		    }
		    else
		    {
	            	equipBTN.interactable = false;
		    }
	    	}
	    	useBTN.gameObject.SetActive(false);
	    	if (_slot.itemObject.type == ItemType.Weapon)
	    	{
	    	    weaponStatus.gameObject.SetActive(true);
	    	    armorStatus.gameObject.SetActive(false);
	    	    consumableStatus.gameObject.SetActive(false);
	    	    weaponStatus.ListStatus(_slot.item);
	    	}
	    	else if (_slot.itemObject.type == ItemType.Helmet || _slot.itemObject.type == ItemType.Chest || _slot.itemObject.type == ItemType.Pants)
	    	{
	    	    weaponStatus.gameObject.SetActive(false);
	    	    armorStatus.gameObject.SetActive(true);
	    	    consumableStatus.gameObject.SetActive(false);
	    	    armorStatus.ListStatus(_slot.item);
	    	}
	    }
	}
	
	foreach (ItemType type in consumableItems)
	{
	    if (_slot.itemObject.type == type)
	    {
		itemLevelBox.SetActive(true);
		itemLevel.text = "Nv " + _slot.itemObject.level.ToString();
	    	useBTN.gameObject.SetActive(true);
		if ( _slot.itemObject.level > FindObjectOfType<PlayerStats>().level)
		{
		    itemLevel.color = unavailableLevelColor;
	            useBTN.interactable = false;
		}
		else
		{
		    itemLevel.color = availableLevelColor;
	            useBTN.interactable = true;
		}
		equipBTN.gameObject.SetActive(false);
	    	unequipBTN.gameObject.SetActive(false);
	    	weaponStatus.gameObject.SetActive(false);
	    	armorStatus.gameObject.SetActive(false);
	    	consumableStatus.gameObject.SetActive(true);
	    	consumableStatus.ListStatus(_slot.item);
	    }
	}
    }

    public void Reset()
    {
	itemNameBox.SetActive(false);
	itemLevelBox.SetActive(false);
	equipBTN.gameObject.SetActive(false);
	unequipBTN.gameObject.SetActive(false);
	useBTN.gameObject.SetActive(false);
	weaponStatus.gameObject.SetActive(false);
	armorStatus.gameObject.SetActive(false);
	consumableStatus.gameObject.SetActive(false);
	clicked = false;
    }

    public void Click()
    {
	clicked = true;
    }
}
