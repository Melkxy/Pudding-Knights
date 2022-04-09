using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    [SerializeField]private int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
	if(other.gameObject.CompareTag("Player"))
	{
	    other.GetComponent<PlayerStats>().DamageHP(damage);
	}
    }
}
