using UnityEngine;

public class Enemy : BaseCharacter
{
    public float moveSpeed = 3f;
    public float desiredDistance = 2f;  // The distance the enemy will try to maintain from the player
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float nextFireTime = 0f;

    private Rigidbody2D rb;
    private Transform player;

    // Reference to the GameObject that should be destroyed or disabled when this enemy dies
    public GameObject restrictedAreaObject;  // Assign this in the Unity Editor or dynamically

    protected override void Start()
    {
        base.Start();  // Calls the Start function from BaseCharacter to initialize health
        rb = GetComponent<Rigidbody2D>();

        // Try to find the player using the tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player is tagged as 'Player'.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Move only if the enemy is farther than the desired distance
            if (distanceToPlayer > desiredDistance)
            {
                MoveTowardPlayer();
            }
            else
            {
                // Optionally face the player but don't move closer
                FacePlayer();
            }

            ShootAtPlayer();  // Continue shooting regardless of distance
        }
    }

    private void MoveTowardPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
    }

    private void FacePlayer()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    private void ShootAtPlayer()
    {
        if (Time.time >= nextFireTime && projectilePrefab != null && firePoint != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = projectile.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Initialize(direction, "Player");  // Targeting the player
            }

            nextFireTime = Time.time + fireRate;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Enemy collided with a wall.");
            // Optionally, stop movement or reverse direction here
        }
        else
        {
            Debug.Log("Enemy collided with: " + collision.gameObject.name);
        }
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Enemy has been killed.");

        // Check if the enemy is supposed to open the restricted area
        if (restrictedAreaObject != null)
        {
            // Destroy the restricted area object (like a door or barrier) when the enemy dies
            Destroy(restrictedAreaObject);
            Debug.Log("Restricted area opened!");
        }

        // Notify the player that an enemy has been killed
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            Debug.Log("Enemy killed, notifying player.");
            player.EnemyKilled();  // Increment the kill count in the player script
        }

        // Destroy the enemy object after death
        Destroy(gameObject);
    }
}
