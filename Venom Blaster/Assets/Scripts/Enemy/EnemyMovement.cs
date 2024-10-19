using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public GameObject projectilePrefab;   // Drag the projectile prefab here
    public Transform player;              // Reference to the player, can be set in the inspector
    public Transform firePoint;           // Position where the projectile is spawned
    public float fireRate = 1.5f;         // Time between shots

    private float nextFireTime = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            // Automatically find the player if not set in Inspector
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        MoveTowardPlayer();
        ShootAtPlayer();
    }

    void MoveTowardPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
        }
    }

    void ShootAtPlayer()
    {
        if (Time.time >= nextFireTime)
        {
            // Instantiate the projectile at firePoint's position and rotation
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            // Set direction for the projectile towards the player
            Vector2 direction = (player.position - firePoint.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * 10f;  // Set the speed here

            nextFireTime = Time.time + fireRate;
        }
    }
}
