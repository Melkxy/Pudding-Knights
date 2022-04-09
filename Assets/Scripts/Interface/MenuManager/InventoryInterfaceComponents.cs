using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryInterfaceComponents : MonoBehaviour
{
    private PlayerStats player;

    [SerializeField]private TextMeshProUGUI attack, defense, attackSpeed, strength;

    private void Awake()
    {
	player = FindObjectOfType<PlayerStats>();
    }

    public void UpdateComponents()
    {
	attack.text = player.playerDmg.value.modifiedValue.ToString();
	defense.text = player.defense.value.modifiedValue.ToString();
	attackSpeed.text = player.attackSpeed.value.modifiedValue.ToString();
	strength.text = player.strength.value.modifiedValue.ToString();
    }
}
