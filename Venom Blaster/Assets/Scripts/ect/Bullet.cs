using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;            // Damage value for the bullet
    public float bulletSpeed = 5f;     // Speed of the bullet
    private Transform target;          // The target for the bullet (can be player or enemy)
    private Rigidbody2D rb;
    private string targetTag;          // The tag of the target to avoid hitting the shooter itself

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // If the bullet has a target set, move towards it
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }

    // Initialize method to set up the bullet's direction and target
    public void Initialize(Vector2 direction, string targetTag)
    {
        this.targetTag = targetTag;  // Set the target tag to avoid self-damage

        // Set bullet's velocity in the given direction
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * bulletSpeed;
    }

    // This method will be called when the bullet collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the correct target tag (e.g., "Player" or "Enemy")
        if (collision.CompareTag(targetTag))
        {
            // Try to get the BaseCharacter component from the collided object (for enemies/players)
            BaseCharacter character = collision.GetComponent<BaseCharacter>();
            if (character != null)
            {
                character.TakeDamage(damage);  // Apply damage to the BaseCharacter
            }

            // Destroy the bullet after hitting the target
            Destroy(gameObject);
        }

        // Check if the collided object is a damageable object (e.g., a door, barrier, etc.)
        DamageableObject damageableObject = collision.GetComponent<DamageableObject>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);  // Apply damage to the damageable object
        }

        // Destroy the bullet after collision with anything
        Destroy(gameObject);
    }
}
