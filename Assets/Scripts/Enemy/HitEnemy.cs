using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    private PlayerStats player;

    private void Awake()
    {
	player = GameObject.FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	if(other.gameObject.CompareTag("Enemy"))
	{
	    other.gameObject.GetComponent<EnemyStats>().TakeDamage(player.playerDmg.value.modifiedValue);
	    StartCoroutine(other
                        .gameObject.transform
                        .Find("ViewRange")
                        .GetComponent<SimpleEnemyAI>()
                        .Knockback(0.25f, player.strength.value.modifiedValue*10, GameObject.FindObjectOfType<PlayerStats>().transform)
                        );
	}
    }
}
