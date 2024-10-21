using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;   // Maximum health of the enemy
    private int currentHealth;  // Current health of the enemy

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);  // Destroy the enemy object when health reaches 0
    }
}
