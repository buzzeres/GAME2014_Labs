using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private Player playerComponent;
    private Enemy enemyComponent;

    private void Start()
    {
        // Try to get the Player or Enemy component depending on which object this is attached to
        playerComponent = GetComponent<Player>();
        enemyComponent = GetComponent<Enemy>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle different collision responses based on the tag of the collided object
        switch (collision.gameObject.tag)
        {
            case "Wall":
                HandleWallCollision(collision);
                break;

            case "Player":
                if (enemyComponent != null)
                {
                    HandleEnemyCollisionWithPlayer(collision);
                }
                break;

            case "Enemy":
                if (playerComponent != null)
                {
                    HandlePlayerCollisionWithEnemy(collision);
                }
                break;

            default:
                HandleDefaultCollision(collision);
                break;
        }
    }

    // Handle collision with walls (for both Player and Enemy)
    private void HandleWallCollision(Collision2D collision)
    {
        Debug.Log(gameObject.name + " collided with a wall: " + collision.gameObject.name);
        // Add additional behavior for wall collisions, like stopping movement or playing sound

        // Example: If the enemy collides with a wall, stop its movement
        if (enemyComponent != null)
        {
            Rigidbody2D enemyRb = enemyComponent.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                enemyRb.velocity = Vector2.zero; // Stop the enemy's movement on collision with the wall
            }
        }
    }

    // Handle collision between Player and Enemy
    private void HandlePlayerCollisionWithEnemy(Collision2D collision)
    {
        Debug.Log("Player collided with an enemy: " + collision.gameObject.name);
        // Example: Deal damage to the player if an enemy collides with them
        if (playerComponent != null && collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            playerComponent.TakeDamage(10);  // Adjust the damage as needed
        }
    }

    // Handle collision between Enemy and Player
    private void HandleEnemyCollisionWithPlayer(Collision2D collision)
    {
        Debug.Log("Enemy collided with the player: " + collision.gameObject.name);
        // Example: Deal damage to the player if an enemy collides with them
        if (enemyComponent != null && collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(10);  // Adjust the damage as needed
        }
    }

    // Default handler for other collisions
    private void HandleDefaultCollision(Collision2D collision)
    {
        Debug.Log(gameObject.name + " collided with something: " + collision.gameObject.name);
        // Add default behavior for untagged or other objects
    }
}
