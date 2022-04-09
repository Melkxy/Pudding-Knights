using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemArmorStatus : ItemStatus
{
    [SerializeField]private TMP_Text armorDefense, armorSpeed, armorWeight;
    [SerializeField]private AttributeType defenseType, attackSpeedType, strengthType;
    
    public override void ListStatus(Item item)
    {
	armorDefense.text = item.GetItemBuff(defenseType).ToString();
	armorSpeed.text = item.GetItemBuff(attackSpeedType).ToString();
	armorWeight.text = item.GetItemBuff(strengthType).ToString();
    }
}
