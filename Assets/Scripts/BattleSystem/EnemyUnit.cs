using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "BattleSystem/Enemy")]
public class EnemyUnit : ScriptableObject
{
    [SerializeField]private GameObject _enemyPrefab;
    public GameObject enemyPrefab { get { return _enemyPrefab; } private set { _enemyPrefab = value; } }
    [SerializeField]private string _name;
    public new string name { get { return _name; } private set { _name = value; } }
    [SerializeField]private int _maxHP, _currentHP;
    public int maxHP { get { return _maxHP; } private set { _maxHP = value; } }
    public int currentHP { get { return _currentHP; } private set { _currentHP = value; } }
    [SerializeField]private int _damage;
    public int damage { get { return _damage; } private set { _damage = value; } }
    [SerializeField]private int _speed;
    public int speed { get { return _speed; } private set { _speed = value; } }
    [SerializeField]private int _defense;
    public int defense { get { return _defense; } private set { _defense = value; } }

    public EnemyUnit()
    {
	name = "";
	maxHP = 0;
	currentHP = 0;
	damage = 0;
	speed = 0;
	defense = 0;
    }

    public EnemyUnit(EnemyUnit _enemy)
    {
	name = _enemy.name;
	maxHP = _enemy.maxHP;
	currentHP = _enemy.currentHP;
	damage = _enemy.damage;
	speed = _enemy.speed;
	defense = _enemy.defense;
    }

    public void TakeDamage(int dmg)
    {
	if (currentHP <= dmg)
	{
	    currentHP = 0;
	}
	else
	{
	    currentHP -= dmg;
	}
    }

    public void RecoverHP(int recover)
    {
	if (currentHP + recover >= maxHP)
	{
	    currentHP = maxHP;
	}
	else
	{
	    currentHP += recover;
	}
    }
}
