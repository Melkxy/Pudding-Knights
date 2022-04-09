using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class InventoryInterface : MonoBehaviour
{
    [SerializeField]protected InventoryObject _inventory;
    public InventoryObject inventory { get { return _inventory; } private set { _inventory = value; } }

    [SerializeField]protected bool canDragItems;

    [SerializeField]protected GameObject floatingItemLevel;

    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    protected ItemDetailsInterface _details;
    public ItemDetailsInterface details { get { return _details; } protected set { _details = value; } }

    private InventoryInterfaceComponents inventoryComponents;

    protected void Awake()
    {
	details = FindObjectOfType<ItemDetailsInterface>();
	inventoryComponents = FindObjectOfType<InventoryInterfaceComponents>();

	for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
        }

	CreateSlots();

	UpdateSlotDisplays(slotsOnInterface);

	if (canDragItems)
	{
	    AddEvent(gameObject, EventTriggerType.PointerClick, delegate { OnClick(gameObject); });
            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
	}
    }

    public void OnSlotUpdate(InventorySlot _slot)
    {
	if (_slot.item.id >= 0)
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.itemObject.icon;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
        }
        else
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";                
        }
    }

    public void UpdateSlotDisplays(Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            if (_slot.Value.item.id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.itemObject.icon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";                
            }
        }
    }

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<InventoryInterface>();
    }

    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }

    public void OnClick(GameObject obj)
    {
	if (obj == null || obj.Equals(null))
	{
	    return;
	}
	MouseData.slotSelected = obj;
	MouseData.interfaceItemSelected = this;
	if (details != null)
	{
	    details.Reset();
	    details.ShowUp(slotsOnInterface[obj]);
	    details.Click();
	}
    }

    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
	if (details != null)
	{
	    if (!details.clicked)
	    {
		details.Reset();
	        details.ShowUp(slotsOnInterface[obj]);
	    }
	}
    }

    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
	if (details != null)
	{
	    if (!details.clicked)
	    {
		details.Reset();
	    }
	}
    }

    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }

    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;

        if (slotsOnInterface[obj].item.id >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt = obj.transform.GetChild(0).GetComponent<RectTransform>();
            tempItem.transform.SetParent(floatingItemLevel.transform);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].itemObject.icon;
            img.raycastTarget = false;
        }
        return tempItem;
    }

    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);
	if (details != null)
	{
	    details.Reset();
	}

        if (MouseData.interfaceMouseIsOver == null || MouseData.interfaceMouseIsOver.Equals(null))
        {
            /* slotsOnInterface[obj].RemoveItem(); */
            return;
        }
        if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
	    inventoryComponents.UpdateComponents();
        }
    }

    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public abstract void CreateSlots();
}

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            if (_slot.Value.item.id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.itemObject.icon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";                
            }
        }
    }
}
