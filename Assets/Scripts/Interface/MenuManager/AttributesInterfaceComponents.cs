using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttributesInterfaceComponents : MonoBehaviour
{
    private PlayerStats player;

    [SerializeField]private TextMeshProUGUI level;
    [SerializeField]private TextMeshProUGUI xp;
    [SerializeField]private TextMeshProUGUI attributePoints;
    [SerializeField]private Button[] upAttributesBTNs;
    [SerializeField]private TextMeshProUGUI maxHP, currentHP, maxMP, currentMP;
    [SerializeField]private TextMeshProUGUI damageText, defenseText, HealthText, ManaText;

    private void Awake()
    {
	player = FindObjectOfType<PlayerStats>();
    }

    public void UpdateComponents()
    {
	level.text = player.level.ToString();
	xp.text = string.Concat(player.xp.ToString(), " / ", player.GetNeededXP().ToString());
	attributePoints.text = player.attributePoints.ToString();
	SetAttributeButtons();
	maxHP.text = player.maxHP.ToString();
	maxMP.text = player.maxMP.ToString();
	currentHP.text = player.currentHP.ToString();
	currentMP.text = player.currentMP.ToString();
	damageText.text = player.playerDmg.value.baseValue.ToString();
	defenseText.text = player.defense.value.baseValue.ToString();
	HealthText.text = player.maxHP.ToString();
	ManaText.text = player.maxMP.ToString();
    }

    private void SetAttributeButtons()
    {
	if (player.attributePoints > 0)
	{
	    foreach (Button btn in upAttributesBTNs)
	    {
		btn.interactable = true;
	    }
	}
	else
	{
	    foreach (Button btn in upAttributesBTNs)
	    {
		btn.interactable = false;
	    }
	}
    }
}
