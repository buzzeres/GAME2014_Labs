using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private Enemy enemyToActivate;  // Reference to the enemy that will be activated

    // Method to assign an enemy to this trigger zone
    public void AssignEnemy(Enemy enemy)
    {
        enemyToActivate = enemy;
    }

    // This method is called when something enters the trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player has entered the trigger zone
        if (collision.CompareTag("Player") && enemyToActivate != null)
        {
            // Activate the enemy movement
            enemyToActivate.ActivateEnemyMovement();

            // Destroy or deactivate the trigger zone after it's triggered
            Destroy(gameObject);  // Remove the trigger zone
        }
    }
}
