using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemWeaponStatus : ItemStatus
{
    [SerializeField]private TMP_Text weaponDamage, weaponSpeed, weaponWeight;
    [SerializeField]private AttributeType damageType, attackSpeedType, strengthType;
    
    public override void ListStatus(Item item)
    {
	weaponDamage.text = item.GetItemBuff(damageType).ToString();
	weaponSpeed.text = item.GetItemBuff(attackSpeedType).ToString();
	weaponWeight.text = item.GetItemBuff(strengthType).ToString();
    }
}
