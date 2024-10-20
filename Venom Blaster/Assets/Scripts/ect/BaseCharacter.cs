using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public int maxHealth = 100;               // Maximum health value
    public int currentHealth;                 // Current health value
    public HealthSystem healthSystem;         // Reference to the health system for this character

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        // Initialize health system
        if (healthSystem != null)
        {
            healthSystem.maxHealth = maxHealth;
            healthSystem.currentHealth = currentHealth;
            healthSystem.UpdateHealthUI();  // Initialize the health bar at full
        }
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Prevent health from going below 0
        UpdateHealthSystem();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Prevent overhealing
        UpdateHealthSystem();
    }

    protected void UpdateHealthSystem()
    {
        if (healthSystem != null)
        {
            healthSystem.currentHealth = currentHealth;
            healthSystem.UpdateHealthUI();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        // Add death-related behavior here (e.g., destroy character, game over for player, etc.)
        Destroy(gameObject);
    }
}
