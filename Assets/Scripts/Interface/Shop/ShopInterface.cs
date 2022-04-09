using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ShopInterface : MonoBehaviour
{
    [SerializeField]private ShopObject _shop;
    public ShopObject shop { get { return _shop; } private set { _shop = value; } }

    [SerializeField]private GameObject shopSlotPrefab;

    public Dictionary<GameObject, ShopSlot> slotsOnInterface = new Dictionary<GameObject, ShopSlot>();

    protected ShopItemDetailsInterface _details;
    public ShopItemDetailsInterface details { get { return _details; } protected set { _details = value; } }

    private void Awake()
    {
	details = FindObjectOfType<ShopItemDetailsInterface>();

	AddEvent(gameObject, EventTriggerType.PointerClick, delegate { OnClick(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });

	details.Reset();
    }

    public void OpenShop(ShopObject shopObject)
    {
	shop = shopObject;

	for (int i = 0; i < shop.GetSlots.Length; i++)
	{
	    shop.GetSlots[i].parent = this;
            shop.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
	}

	CreateSlots();

	UpdateSlotDisplays(slotsOnInterface);
    }

    public void OnSlotUpdate(ShopSlot _slot)
    {
	if (_slot.itemObject.data.id >= 0)
        {
            _slot.slotDisplay.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = _slot.itemObject.icon;
            _slot.slotDisplay.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
	    _slot.slotDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _slot.itemObject.data.name;
	    _slot.slotDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _slot.itemObject.data.price.ToString();
        }
        else
        {
            _slot.slotDisplay.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "";     
	    _slot.slotDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
	    _slot.slotDisplay.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";           
        }
    }

    public void UpdateSlotDisplays(Dictionary<GameObject, ShopSlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, ShopSlot> _slot in _slotsOnInterface)
        {
            if (_slot.Value.itemObject.data.id >= 0)
            {
                _slot.Key.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.itemObject.icon;
                _slot.Key.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
	    	_slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _slot.Value.itemObject.data.name;
	    	_slot.Key.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _slot.Value.itemObject.data.price.ToString();
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "";  
	    	_slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
	    	_slot.Key.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";                
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
        MouseData.shopInterfaceMouseIsOver = obj.GetComponent<ShopInterface>();
    }

    public void OnExitInterface(GameObject obj)
    {
        MouseData.shopInterfaceMouseIsOver = null;
    }

    public void OnClick(GameObject obj)
    {
	if (obj == null || obj.Equals(null))
	{
	    return;
	}
	MouseData.shopSlotSelected = obj;
	MouseData.shopInterfaceItemSelected = this;
	if (details != null)
	{
	    details.Reset();
	    details.ShowUp(slotsOnInterface[obj]);
	    details.Click();
	}
    }

    public void OnEnter(GameObject obj)
    {
        MouseData.shopSlotHoveredOver = obj;
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
        MouseData.shopSlotHoveredOver = null;
	if (details != null)
	{
	    if (!details.clicked)
	    {
		details.Reset();
	    }
	}
    }

    public void CreateSlots()
    {
	foreach (Transform child in transform)
	{
	    Destroy(child.gameObject);
	}

	slotsOnInterface = new Dictionary<GameObject, ShopSlot>();

        for (int i = 0; i < shop.GetSlots.Length; i++)
        {
            var obj = Instantiate(shopSlotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = new Vector3(i*0.5f,0f,0f);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });

            shop.GetSlots[i].slotDisplay = obj;

            slotsOnInterface.Add(obj, shop.GetSlots[i]);
        }
    }
}
