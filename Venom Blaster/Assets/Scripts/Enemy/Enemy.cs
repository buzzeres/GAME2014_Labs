using UnityEngine;

public class Enemy : BaseCharacter
{
    public float moveSpeed = 3f;
    public float desiredDistance = 2f;  // The distance the enemy will try to maintain from the player
    public float shootingDistance = 5f;  // The distance within which the enemy will shoot at the player
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 4.5f;
    private float nextFireTime = 0f;
    public bool isSpecialEnemy = false; // Mark if this is the enemy for the win condition
    private bool canMove = false; // Enemy starts stationary until triggered

    private Rigidbody2D rb;
    private Transform player;

    // Reference to the Trigger Area (you will drag and drop in the Unity editor)
    public GameObject triggerZone; // Assign this trigger area via the editor

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

        // If the trigger zone exists, ensure it has a TriggerZone script attached
        if (triggerZone != null)
        {
            TriggerZone trigger = triggerZone.GetComponent<TriggerZone>();
            if (trigger != null)
            {
                trigger.AssignEnemy(this); // Link the trigger zone to this specific enemy
            }
            else
            {
                Debug.LogError("TriggerZone component not found on the trigger zone object.");
            }
        }
    }

    private void Update()
    {
        if (!canMove) return;  // Exit update if the enemy can't move yet

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
                // Face the player but don't move closer
                FacePlayer();
            }

            // Only shoot if the player is within the shooting distance
            if (distanceToPlayer <= shootingDistance)
            {
                ShootAtPlayer();
            }
        }
    }

    private void MoveTowardPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
    }

    // Face the player before shooting or moving, and rotate the firePoint with the enemy
    private void FacePlayer()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;  // Rotate the enemy to face the player

            // Rotate the firePoint to match the enemy's rotation
            firePoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
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
            // Stop the enemy when it collides with a wall
            rb.velocity = Vector2.zero;
        }
        else
        {
            Debug.Log("Enemy collided with: " + collision.gameObject.name);
        }
    }

    protected override void Die()
    {
        base.Die();
        if (isSpecialEnemy)
        {
            // Call the GameOverManager's Win function when the special enemy dies
            GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
            if (gameOverManager != null)
            {
                gameOverManager.TriggerWinCondition();
            }
        }

        // Notify the player for regular enemies
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.EnemyKilled();
        }

        Destroy(gameObject);
    }

    // Method to start moving the enemy when the trigger is activated
    public void ActivateEnemyMovement()
    {
        canMove = true;
    }
}
