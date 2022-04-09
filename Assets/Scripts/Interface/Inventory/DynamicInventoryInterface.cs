using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInventoryInterface : InventoryInterface
{
    [SerializeField]private GameObject inventoryPrefab;

    public override void CreateSlots()
    {
	details = FindObjectOfType<ItemDetailsInterface>();

        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = new Vector3(i*0.5f,0f,0f);

	    if (canDragItems)
	    {
            	AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            	AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            	AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            	AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            	AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            	AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
	    }

            inventory.GetSlots[i].slotDisplay = obj;

            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }
}
