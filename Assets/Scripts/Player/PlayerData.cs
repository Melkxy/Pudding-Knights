using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField]private int _playerLevel;
    public int playerLevel { get { return _playerLevel; } private set { _playerLevel = value; } }

    [SerializeField]private int _playerXP, _playerBaseXP;
    public int playerXP { get { return _playerXP; } private set { _playerXP = value; } }
    public int playerBaseXP { get { return _playerBaseXP; } private set { _playerBaseXP = value; } }

    [SerializeField]private int _playerAttributePoints;
    public int playerAttributePoints { get { return _playerAttributePoints; } private set { _playerAttributePoints = value; } }

    [SerializeField]private int _playerMaxHP, _playerMaxMP;
    public int playerMaxHP { get { return _playerMaxHP; } private set { _playerMaxHP = value; } }
    public int playerMaxMP { get { return _playerMaxMP; } private set { _playerMaxMP = value; } }

    [SerializeField]private int _playerCurrentHP, _playerCurrentMP;
    public int playerCurrentHP { get { return _playerCurrentHP; } private set { _playerCurrentHP = value; } }
    public int playerCurrentMP { get { return _playerCurrentMP; } private set { _playerCurrentMP = value; } }

    [SerializeField]private int _playerDmg, _playerStrength, _playerDefense, _playerAttackSpeed;
    public int playerDmg { get { return _playerDmg; } private set { _playerDmg = value; } }
    public int playerStrength { get { return _playerStrength; } private set { _playerStrength = value; } }
    public int playerDefense { get { return _playerDefense; } private set { _playerDefense = value; } }
    public int playerAttackSpeed { get { return _playerAttackSpeed; } private set { _playerAttackSpeed = value; } }

    [SerializeField]private int _playerMoney;
    public int playerMoney { get { return _playerMoney; } private set { _playerMoney = value; } }

    public PlayerData(Player player)
    {
	playerLevel = player.playerStats.level;
	playerXP = player.playerStats.xp;
	playerBaseXP = player.playerStats.baseXP;
	playerAttributePoints = player.playerStats.attributePoints;
	playerMaxHP = player.playerStats.maxHP;
	playerMaxMP = player.playerStats.maxMP;
	playerCurrentHP = player.playerStats.currentHP;
	playerCurrentMP = player.playerStats.currentMP;
	playerDmg = player.playerStats.playerDmg.value.baseValue;
	playerStrength = player.playerStats.strength.value.baseValue;
	playerDefense = player.playerStats.defense.value.baseValue;
	playerAttackSpeed = player.playerStats.attackSpeed.value.baseValue;
	playerMoney = player.playerMoney.money;
    }
}
