using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]protected GameObject enemyPrefab;
    [SerializeField]protected Transform spawnPosition;

    public void SpawnEnemy()
    {
	Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity);
    }
}
