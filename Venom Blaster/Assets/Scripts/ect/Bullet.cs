using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;  // Damage value for the bullet
    public float bulletSpeed = 5f;  // Speed of the bullet (adjust this to control speed)
    private Rigidbody2D rb;
    private Transform target;  // Target the bullet will move toward
    private string targetTag;  // The tag of the target (e.g., "Player" or "Enemy")

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // If a target exists, move toward it
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    // Initialize method to set up the bullet's direction and target
    public void Initialize(Vector2 direction, string targetTag)
    {
        this.targetTag = targetTag;

        // Set bullet's velocity in the given direction (normalized)
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * bulletSpeed;
    }

    // Method to move the bullet toward the set target (for tracking a specific target)
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        // Start moving toward the target
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    // Move the bullet toward the target
    private void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    // This method will be called when the bullet collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the correct target tag
        if (collision.CompareTag(targetTag))
        {
            // Try to get the BaseCharacter component from the collided object
            BaseCharacter character = collision.GetComponent<BaseCharacter>();

            // If the character is found, apply damage
            if (character != null)
            {
                character.TakeDamage(damage);  // Apply damage to the character
            }

            // Destroy the bullet after it hits the target
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            // Destroy the bullet if it hits a wall
            Destroy(gameObject);
        }
    }
}
