using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // Reference to the enemy prefab
    public Transform spawnArea;      // The big circle (spawn area)

    private int enemiesToSpawn = 1;  // Number of enemies to spawn
    private int currentEnemyCount = 0;  // Keep track of currently active enemies

    void Start()
    {
        // Spawn the first enemy at the beginning
        SpawnEnemy();
    }

    void Update()
    {
        // If there are no more enemies left, spawn the next wave
        if (currentEnemyCount <= 0)
        {
            enemiesToSpawn *= 2;  // Double the number of enemies to spawn
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        // Spawn at a random position within the big circle
        Vector2 randomPosition = Random.insideUnitCircle * (spawnArea.localScale.x / 2);
        GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);

        currentEnemyCount++;  // Increase the enemy count

        // Assign the spawner to the enemy so it can notify when it dies
        newEnemy.GetComponent<Enemy>().spawner = this;
    }

    // This method will be called by the enemy when it's destroyed
    public void EnemyKilled()
    {
        currentEnemyCount--;  // Decrease the enemy count
    }
}
