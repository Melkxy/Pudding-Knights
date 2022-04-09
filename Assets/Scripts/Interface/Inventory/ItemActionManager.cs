using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionManager : MonoBehaviour
{
    [SerializeField]private InventoryInterface equipmentInterface;
    [SerializeField]private InventoryInterface[] inventorysInterfaces;
    private ItemDetailsInterface details;

    private void Awake()
    {
	details = GetComponent<ItemDetailsInterface>();
    }

    public void UseSelectedItem()
    {
	MouseData.interfaceItemSelected.inventory.UseItem(MouseData.interfaceItemSelected.slotsOnInterface[MouseData.slotSelected]);
	if (MouseData.interfaceItemSelected.slotsOnInterface[MouseData.slotSelected].amount <= 0 && MouseData.interfaceItemSelected.details)
	{
	    MouseData.interfaceItemSelected.details.Reset();
	}
	FindObjectOfType<InventoryInterfaceComponents>().UpdateComponents();
    }

    public void EquipSelectedItem()
    {
	foreach (var slot in equipmentInterface.slotsOnInterface.Values)
	{
	    bool swapped = MouseData.interfaceItemSelected.inventory.SwapItems(MouseData.interfaceItemSelected.slotsOnInterface[MouseData.slotSelected], slot);
	    if (swapped)
	    {
		if (MouseData.interfaceItemSelected.details)
		{
		    MouseData.interfaceItemSelected.details.Reset();
		}
		FindObjectOfType<InventoryInterfaceComponents>().UpdateComponents();
		return;
	    }
	}
    }

    public void UnequipSelectedItem()
    {
	foreach(InventoryInterface inventoryInterface in inventorysInterfaces)
	{
	    foreach(ItemType itemType in inventoryInterface.inventory.Container.allowedItems)
	    {
		if (itemType == MouseData.interfaceItemSelected.slotsOnInterface[MouseData.slotSelected].itemObject.type)
		{
		    foreach(var slot in inventoryInterface.slotsOnInterface.Values)
		    {
			if (slot.item.id <= -1)
			{
		            bool swapped = MouseData.interfaceItemSelected.inventory.SwapItems(MouseData.interfaceItemSelected.slotsOnInterface[MouseData.slotSelected], slot);
	    	            if (swapped)
	    	            {
			     	if (MouseData.interfaceItemSelected.details)
			    	{
		   	            MouseData.interfaceItemSelected.details.Reset();
			        }
			    	FindObjectOfType<InventoryInterfaceComponents>().UpdateComponents();
			    	return;
	    	            }
			}
		    }
		}
	    }
	}
    }
}
