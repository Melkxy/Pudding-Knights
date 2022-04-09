using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemConsumableStatus : ItemStatus
{
    [SerializeField]private TMP_Text healthRecovery, manaRecovery;
    [SerializeField]private ItemRecoveryType health, mana;
    
    public override void ListStatus(Item item)
    {
	healthRecovery.text = item.GetItemRecovery(health).ToString();
	manaRecovery.text = item.GetItemRecovery(mana).ToString();
    }
}
