using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private string _savePath;
    public string savePath { get { return _savePath; } private set { _savePath = value; } }
    [SerializeField]private InventoryObject _equipment;
    public InventoryObject equipment { get { return _equipment; } private set { _equipment = value; } }
    [SerializeField]private InventoryObject[] _inventories;
    public InventoryObject[] inventories { get { return _inventories; } private set { _inventories = value; } }
    [SerializeField]private PlayerStats _playerStats;
    public PlayerStats playerStats { get { return _playerStats; } private set { _playerStats = value; } }
    [SerializeField]private PlayerMoney _playerMoney;
    public PlayerMoney playerMoney { get { return _playerMoney; } private set { _playerMoney = value; } }

    [SerializeField]private GameObject _hint;
    public GameObject hint { get { return _hint; } private set { _hint = value; } }

    private void Awake()
    {
	playerStats = GetComponent<PlayerStats>();
	playerMoney = GetComponent<PlayerMoney>();
        playerStats.playerDmg.SetParent(this);
        playerStats.defense.SetParent(this);
        playerStats.strength.SetParent(this);
        playerStats.attackSpeed.SetParent(this);
	UpdateEquipmentSlots();
    }

    public void UpdateEquipmentSlots()
    {
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.itemObject == null || _slot.itemObject.Equals(null))
        {
            return;
        }
        switch (_slot.parent.inventory.inventoryType)
        {
	    case InventoryType.Equipment:
                Debug.Log(string.Concat("Removed ", _slot.itemObject, " on ", _slot.parent.inventory.inventoryType, ", Allowed items: ", string.Join(", ", _slot.allowedItems)));
                
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
		    switch (_slot.item.buffs[i].attribute)
		    {
		        case AttributeType.Damage:
			    playerStats.playerDmg.value.RemoveModifier(_slot.item.buffs[i]);
                	    break;
		        case AttributeType.Defense:
			    playerStats.defense.value.RemoveModifier(_slot.item.buffs[i]);
                	    break;
		        case AttributeType.Strength:
			    playerStats.strength.value.RemoveModifier(_slot.item.buffs[i]);
                	    break;
		        case AttributeType.AttackSpeed:
			    playerStats.attackSpeed.value.RemoveModifier(_slot.item.buffs[i]);
                	    break;
		        default:
			    break;
		    }
	        }
                break;
	    default:
		break;
        }
    }

    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.itemObject == null || _slot.itemObject.Equals(null))
        {
            return;
        }
        switch (_slot.parent.inventory.inventoryType)
        {
            case InventoryType.Equipment:
                Debug.Log(string.Concat("Placed ", _slot.itemObject, " on ", _slot.parent.inventory.inventoryType, ", Allowed items: ", string.Join(", ", _slot.allowedItems)));
                
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
		    switch (_slot.item.buffs[i].attribute)
		    {
		        case AttributeType.Damage:
			    playerStats.playerDmg.value.AddModifier(_slot.item.buffs[i]);
                	    break;
		        case AttributeType.Defense:
			    playerStats.defense.value.AddModifier(_slot.item.buffs[i]);
                	    break;
		        case AttributeType.Strength:
			    playerStats.strength.value.AddModifier(_slot.item.buffs[i]);
                	    break;
		        case AttributeType.AttackSpeed:
			    playerStats.attackSpeed.value.AddModifier(_slot.item.buffs[i]);
                	    break;
		        default:
			    break;
		    }
	        }
                break;
	    default:
		break;
        }
    }

    public void AttributeModified(PlayerAttribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " updated! Its value is " + attribute.value.modifiedValue));
    }

    public bool HasWeapon()
    {
	foreach (InventorySlot slot in equipment.GetSlots)
	{
	    foreach(ItemType type in slot.allowedItems)
	    {
		if (type == ItemType.Weapon)
		{
	    	    if (slot.item.id > -1)
	    	    {
			return true;
	    	    }
		}
	    }
	}
	return false;
    }

    [ContextMenu("Save")]
    public void SavePlayer()
    {
	SaveSystem.SavePlayer(this);
    }

    [ContextMenu("Load")]
    public void LoadPlayer()
    {
	PlayerData data = SaveSystem.LoadPlayer();
	playerStats.SetStats(data);
	playerMoney.SetMoney(data);
    }

    private void OnApplicationQuit()
    {
	ClearInventories();
    }

    public void ClearInventories()
    {
	foreach (InventoryObject inventory in inventories)
	{
	    inventory.Clear();
	}
	equipment.Clear();
    }
}