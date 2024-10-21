using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health of the object
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;  // Initialize the current health
    }

    // Method to apply damage to the object
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Reduce health by the damage amount
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

        // Check if the object's health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to destroy the object when health is zero
    private void Die()
    {
        Debug.Log($"{gameObject.name} has been destroyed.");
        Destroy(gameObject);  // Destroy the GameObject
    }
}
