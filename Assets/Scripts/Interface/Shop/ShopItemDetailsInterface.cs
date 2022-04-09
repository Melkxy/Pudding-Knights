using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemDetailsInterface : MonoBehaviour
{
    private PlayerMoney player;

    [SerializeField]private TMP_Text itemName;
    [SerializeField]private Button buyBTN;

    [SerializeField]private Button upAmountBTN, downAmountBTN;
    [SerializeField]private GameObject purchaseAmountDisplay;
    [SerializeField]private TMP_Text purchaseAmountText;
    [SerializeField]private GameObject itemPriceDisplay;
    [SerializeField]private TMP_Text itemPriceText;
    [SerializeField]private TMP_Text itemLevelText;
    [SerializeField]private Color itemLevelAvailableColor, itemLevelUnavailableColor;

    [SerializeField]private ItemStatus weaponStatus, armorStatus, consumableStatus;

    [SerializeField]private GameObject detailsSlot;

    [SerializeField]private ItemType[] equipableItems, consumableItems;

    private bool _clicked;
    public bool clicked { get { return _clicked; } private set { _clicked = value; } }

    private int maxPurchaseAmount;
    private int _currentPurchaseAmount;
    public int currentPurchaseAmount { get { return _currentPurchaseAmount; } private set { _currentPurchaseAmount = value; } }

    private ShopSlot _slot;
    public ShopSlot slot { get { return _slot; } private set { _slot = value; } }

    private void Awake()
    {
	player = FindObjectOfType<PlayerMoney>().GetComponent<PlayerMoney>();
    }

    public void ShowUp(ShopSlot _slot)
    {
	slot = _slot;
	if (slot.itemObject == null)
	{
	    return;
	}
	if (!player.HaveEnoughMoney(slot.itemObject.data.price) || slot.itemObject.level > FindObjectOfType<PlayerStats>().level)
	{
	    maxPurchaseAmount = 0;
	    currentPurchaseAmount = 0;
	}
	else if (!slot.itemObject.stackable)
	{
	    maxPurchaseAmount = 1;
	    currentPurchaseAmount = 1;
	}
	else
	{
	    maxPurchaseAmount = player.money / slot.itemObject.data.price;
	    currentPurchaseAmount = 1;
	}
	UpdatePurchaseAmountArea(slot);

	UpdateDetailsSlot();

	itemName.text = slot.itemObject.data.name;
	
	foreach (ItemType type in equipableItems)
	{
	    if (slot.itemObject.type == type)
	    {
	    	if (slot.itemObject.type == ItemType.Weapon)
	    	{
	    	    weaponStatus.gameObject.SetActive(true);
	    	    armorStatus.gameObject.SetActive(false);
	    	    consumableStatus.gameObject.SetActive(false);
	    	    weaponStatus.ListStatus(slot.itemObject.data);
	    	}
	    	else if (slot.itemObject.type == ItemType.Helmet || slot.itemObject.type == ItemType.Chest || slot.itemObject.type == ItemType.Pants)
	    	{
	    	    weaponStatus.gameObject.SetActive(false);
	    	    armorStatus.gameObject.SetActive(true);
	    	    consumableStatus.gameObject.SetActive(false);
	    	    armorStatus.ListStatus(slot.itemObject.data);
	    	}
	    }
	}
	
	foreach (ItemType type in consumableItems)
	{
	    if (slot.itemObject.type == type)
	    {
	    	weaponStatus.gameObject.SetActive(false);
	    	armorStatus.gameObject.SetActive(false);
	    	consumableStatus.gameObject.SetActive(true);
	    	consumableStatus.ListStatus(slot.itemObject.data);
	    }
	}
    }

    private void UpdateDetailsSlot()
    {
	if (slot.itemObject != null)
	{
	    detailsSlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.itemObject.icon;
            detailsSlot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            detailsSlot.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = slot.amount == 1 ? "" : slot.amount.ToString("n0");
	}
	else
	{
            detailsSlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            detailsSlot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            detailsSlot.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "";  
	}
    }

    private void UpdatePurchaseAmountArea(ShopSlot _slot)
    {
	if (slot.itemObject != null)
	{
	    if (currentPurchaseAmount > 0)
	    {
	    	purchaseAmountDisplay.SetActive(true);
	    	purchaseAmountText.text = currentPurchaseAmount.ToString("n0");
	        SetBuyBTN(player.HaveEnoughMoney(slot.itemObject.data.price));
	    }
	    else
	    {
	    	purchaseAmountDisplay.SetActive(false);
	        SetBuyBTN(false);
	    }

	    UpdateUpDownAmountButtons();
	    UpdateItemPrice(_slot);
	    UpdateItemLevel(_slot);
	}
	else
	{
	    purchaseAmountDisplay.SetActive(false);

	    UpdateUpDownAmountButtons();
	    UpdateItemPrice(_slot);
	    buyBTN.gameObject.SetActive(false);
	    itemLevelText.text = "";
	}
    }

    private void UpdateUpDownAmountButtons()
    {
	if (currentPurchaseAmount > 0)
	{
	    upAmountBTN.gameObject.SetActive(true);
	    downAmountBTN.gameObject.SetActive(true);
	    upAmountBTN.interactable = currentPurchaseAmount < maxPurchaseAmount;
	    downAmountBTN.interactable = currentPurchaseAmount > 1;
	}
	else
	{
	    upAmountBTN.gameObject.SetActive(false);
	    downAmountBTN.gameObject.SetActive(false);
	}
    }

    private void UpdateItemPrice(ShopSlot _slot)
    {
	itemPriceDisplay.SetActive(currentPurchaseAmount > 0);
	if (currentPurchaseAmount > 0)
	{
	    int totalPrice = currentPurchaseAmount * _slot.itemObject.data.price;
	    itemPriceText.text = totalPrice.ToString();
	}
    }

    private void UpdateItemLevel(ShopSlot _slot)
    {
	itemLevelText.text = ("Nv " + _slot.itemObject.level.ToString());
	if (slot.itemObject.level > FindObjectOfType<PlayerStats>().level)
	{
	    itemLevelText.color = itemLevelUnavailableColor;
	}
	else
	{
	    itemLevelText.color = itemLevelAvailableColor;
	}
    }

    private void SetBuyBTN(bool active)
    {
	buyBTN.gameObject.SetActive(true);
	buyBTN.interactable = active;
    }

    public void Reset()
    {
	clicked = false;
	slot = new ShopSlot();
	maxPurchaseAmount = 0;
	currentPurchaseAmount = 0;
	UpdatePurchaseAmountArea(slot);
	UpdateDetailsSlot();
	itemName.text = "";
	buyBTN.gameObject.SetActive(false);
	weaponStatus.gameObject.SetActive(false);
	armorStatus.gameObject.SetActive(false);
	consumableStatus.gameObject.SetActive(false);
    }

    public void Click()
    {
	clicked = true;
    }

    public void UpPurchaseAmount()
    {
	if (currentPurchaseAmount < maxPurchaseAmount)
	{
	    currentPurchaseAmount++;
	    UpdatePurchaseAmountArea(slot);
	}
    }

    public void DownPurchaseAmount()
    {
	if (currentPurchaseAmount > 1)
	{
	    currentPurchaseAmount--;
	    UpdatePurchaseAmountArea(slot);
	}
    }
}
