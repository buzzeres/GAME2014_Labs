using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;                 // Maximum health value
    public int currentHealth;                   // Current health value
    public Image greenHealthBarForeground;      // The foreground image (Green) that will shrink based on health

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();  // Initialize the health bar at full
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Ensure health doesn't go below zero
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Prevent overhealing
        UpdateHealthUI();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;  // Reset the player's health to its maximum value
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        if (greenHealthBarForeground != null)
        {
            // Adjust the health bar's fill amount based on current health
            float healthPercentage = (float)currentHealth / (float)maxHealth;
            greenHealthBarForeground.fillAmount = healthPercentage;  // fillAmount ranges from 0 (empty) to 1 (full)
        }
        else
        {
            Debug.LogError("Green Health Bar Foreground is not assigned!");  // Error log if the health bar is not assigned
        }
    }

    private void Die()
    {
        Debug.Log("Character has died.");
        // Add death-related behavior here (e.g., destroy character, game over for player, etc.)
        Destroy(gameObject);
    }
}
