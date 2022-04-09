using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    private int _enemyHP, _maxEnemyHP;
    public int enemyHP { get { return _enemyHP; } private set { _enemyHP = value; } }
    public int maxEnemyHP { get { return _maxEnemyHP; } private set { _maxEnemyHP = value; } }

    [SerializeField]private int enemyDefense;

    private bool invencible;

    private EnemyAI enemyAI;

    [SerializeField]private GroundItemObject[] itemRewards;
    [SerializeField]private int xpReward;
    [SerializeField]private int moneyReward;

    [SerializeField]private GameObject itemBagPrefab;

    private void Awake()
    {
	enemyAI = GetComponentInChildren<EnemyAI>();
    }

    public void TakeDamage(int dmg)
    {
	if (!invencible)
	{
	    int finalDmg = (dmg * 4) - (enemyDefense * 2);
	    if (_enemyHP <= finalDmg)
	    {
	    	_enemyHP = 0;
		StartCoroutine(DeadAction());
	    }
	    else
	    {
	    	_enemyHP -= finalDmg;
	    	enemyAI.PauseEnemy(0.25f);
	    }
	}
    }

    private IEnumerator DeadAction()
    {
	FindObjectOfType<PlayerStats>().GiveXP(xpReward);
	foreach (GroundItemObject item in itemRewards)
	{
	    GameObject itemDrop = Instantiate(itemBagPrefab, transform.position, Quaternion.identity);
	    itemDrop.GetComponent<GroundItem>().SetItemDrop(item);
	}
	FindObjectOfType<PlayerMoney>().GiveMoney(moneyReward);
	yield return new WaitForEndOfFrame();
	Destroy(gameObject); 
    }
}
