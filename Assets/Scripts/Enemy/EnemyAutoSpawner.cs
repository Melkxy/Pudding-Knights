using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoSpawner : EnemySpawner
{
    [SerializeField]private float spawnDelay;
    private float spawnDelayTimer;

    private void Update()
    {
	AutoSpawnEnemy();
    }

    private void AutoSpawnEnemy()
    {
	if (spawnDelayTimer <= 0)
	{
	    spawnDelayTimer = spawnDelay;
	    SpawnEnemy();
	}
	else
	{
	    spawnDelayTimer -= Time.deltaTime;
	}
    }
}
