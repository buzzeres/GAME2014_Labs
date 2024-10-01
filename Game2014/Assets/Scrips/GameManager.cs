using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    [Header("Player & Score Settings")]
    public TextMeshProUGUI scoreText;    
    public int playerScore = 0;         

    [Header("Enemy Settings")]
    public GameObject enemyPrefab;       
    public int totalEnemies = 5;       

    [Header("Boundary Settings")]
    public Boundary horizontalBoundary;  
    public Boundary verticalBoundary;   

    void Start()
    {
        // Spawn the initial set of enemies
        for (int i = 0; i < totalEnemies; i++)
        {
            SpawnEnemy();
        }

        UpdateScoreUI();
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(horizontalBoundary.min, horizontalBoundary.max),
            Random.Range(verticalBoundary.min, verticalBoundary.max),
            0);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Get the EnemyBehavior component and set its boundaries
        EnemyBehavior enemyBehavior = newEnemy.GetComponent<EnemyBehavior>();
        enemyBehavior.SetBoundaries(horizontalBoundary, verticalBoundary);
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + playerScore;
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        Destroy(enemy);        // destroy the enemy GameObject
        playerScore += 10;     // Increase the player's score by 10 points
        UpdateScoreUI();       // update the score ui

        SpawnEnemy();
    }
}
