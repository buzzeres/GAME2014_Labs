using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Make GameManager singleton

    [Header("Player & Score Settings")]
    public TextMeshPro scoreText;
    public TextMeshPro healthText;  // Add healthText field

    public int playerScore = 0;

    [Header("Player Health Settings")]
    public int playerHealth = 3;  // Set player's health to 3
   
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public int maxEnemies = 4;  // Maximum enemies at a time
    private int currentEnemyCount = 0;  // To keep track of active enemies

    [Header("Boundary Settings")]
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;

    private void Awake()
    {
        // Ensure that only one instance of the GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional, if you want to persist the GameManager across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy any duplicate GameManager instances
        }
    }


    private void Start()
    {
        // Spawn the initial set of enemies (4 in this case)
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }

        UpdateScoreUI();  // Only update the score UI now
        UpdateHealthUI();  // Update the health UI
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        Destroy(enemy);  // Destroy the enemy GameObject
        playerScore += 10;  // Increase the player's score by 10 points
        UpdateScoreUI();  // Update the score UI

        // Subtract from the enemy count
        currentEnemyCount--;

        // Respawn a new enemy after 3 seconds delay
        StartCoroutine(RespawnEnemyAfterDelay(3f));
    }

    IEnumerator RespawnEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(horizontalBoundary.min, horizontalBoundary.max),
            verticalBoundary.max,  // Start at the top of the vertical boundary
            0);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Get the EnemyBehavior component and set its boundaries
        EnemyBehavior enemyBehavior = newEnemy.GetComponent<EnemyBehavior>();
        enemyBehavior.SetBoundaries(horizontalBoundary, verticalBoundary);

        // Increase the active enemy count
        currentEnemyCount++;
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + playerScore;  // Only updating score now
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + playerHealth;  // Update the healthText
    }

    public void PlayerHit(int damage)
    {
        Debug.Log("PlayerHit called with damage: " + damage);  // Log the damage value
        playerHealth -= damage;
        Debug.Log("Player Health after hit: " + playerHealth);  // Log current health

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over! Player has been defeated.");
            GameOver();
        }
        else
        {
            UpdateHealthUI();  // Update the health UI if the player is still alive
        }
    }


    public void GameOver()
    {
        // Handle Game Over logic here
        Debug.Log("Game Over! Returning to Main Menu...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
