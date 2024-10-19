using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For handling UI elements like Slider

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float moveSpeed = 3f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float nextFireTime = 0f;

    private Transform player;
    public float stoppingDistance = 2f;

    public EnemySpawner spawner;  // Reference to the spawner
    public Slider healthSlider;   // Reference to the health slider UI

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

        // Set up the health slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();
            ShootAtPlayer();
        }

        // Update health slider UI as enemy takes damage
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    private void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > stoppingDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    private void ShootAtPlayer()
    {
        if (Time.time >= nextFireTime && projectilePrefab != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);  // Destroy the enemy game object

        // Notify the spawner that this enemy has been killed
        if (spawner != null)
        {
            spawner.EnemyKilled();
        }

        // Notify the player to increase the kill count
        Player playerComponent = player.GetComponent<Player>();
        if (playerComponent != null)
        {
            playerComponent.IncreaseKillCount();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            TakeDamage(100);  // Assume the player projectile deals 100 damage
            Destroy(collision.gameObject);  // Destroy the projectile
        }
    }
}
