using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 5f;  // Speed of the projectile
    private Transform player;  // Reference to the player
    private Vector2 target;    // Position to move toward (the player)

    void Start()
    {
        // Find the player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set the target position to the player's current position
        if (player != null)
        {
            target = new Vector2(player.position.x, player.position.y);
        }
    }

    void Update()
    {
        // Move the projectile towards the target
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Destroy the projectile when it reaches the target
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    // On hitting the player, deal damage and destroy the projectile
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(10);  // Damage the player
            Destroy(gameObject);  // Destroy the enemy projectile
        }
    }
}
