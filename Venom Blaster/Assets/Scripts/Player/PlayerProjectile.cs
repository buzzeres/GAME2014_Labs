using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 5f;
    private Transform enemy;
    private Vector2 target;
    private Rigidbody2D rb;
    public float lifeTime = 3f;    // How long the projectile lasts
    public int damage = 1;         // Damage dealt to the enemy

    void Start()
    {
        // Find the nearest enemy when the projectile is instantiated
        enemy = FindNearestEnemy();

        if (enemy != null)
        {
            target = enemy.position; // Set the target to the enemy's position
        }
        else
        {
            // If no enemy is found, shoot straight in the forward direction
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * speed;
        }

        // Destroy the projectile after a set time
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Only move toward the target if an enemy exists
        if (enemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // Destroy the projectile if it reaches the target
            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    private Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);  // Destroy the projectile on impact
        }
    }
}
