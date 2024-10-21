using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player & Score Settings")]
    public TextMeshProUGUI scoreText;
    public int playerScore = 0;
    public int playerHealth = 3;  // Player health added
    public TextMeshProUGUI healthText;  // UI to display player health

    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public int totalEnemies = 5;

    [Header("Boundary Settings")]
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;

    private void Start()
    {
        // Spawn the initial set of enemies
        for (int i = 0; i < totalEnemies; i++)
        {
            SpawnEnemy();
        }

        UpdateScoreUI();
        UpdateHealthUI();
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(horizontalBoundary.min, horizontalBoundary.max),
            verticalBoundary.max,  // Start at the top of the vertical boundary
            0);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Log spawn position for debugging
        Debug.Log("Spawned enemy at: " + spawnPosition);

        // Get the EnemyBehavior component and set its boundaries
        EnemyBehavior enemyBehavior = newEnemy.GetComponent<EnemyBehavior>();
        enemyBehavior.SetBoundaries(horizontalBoundary, verticalBoundary);
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + playerScore;
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + playerHealth;
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        Destroy(enemy);        // Destroy the enemy GameObject
        playerScore += 10;     // Increase the player's score by 10 points
        UpdateScoreUI();       // Update the score UI

        SpawnEnemy();          // Respawn the enemy
    }

    public void PlayerHit()
    {
        playerHealth--;  // Decrease player health when hit by an enemy bullet
        UpdateHealthUI();  // Update the health UI

        if (playerHealth <= 0)
        {
            GameOver();  // Call GameOver when health is zero or below
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! Player has been defeated.");
        // Implement any additional game-over behavior, such as stopping the game, showing a game-over screen, etc.
    }
}
