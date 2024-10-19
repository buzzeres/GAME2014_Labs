using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;          // Drag your health bar slider here
    public Slider cooldownBar;        // Drag your cooldown slider here
    public PlayerHealth playerHealth; // Reference to the Player's health
    public PlayerShooting playerShooting; // Reference to Player's shooting script

    void Update()
    {
        UpdateHealthBar();
        //UpdateCooldownBar();
    }

    void UpdateHealthBar()
    {
        healthBar.value = (float)playerHealth.currentHealth / playerHealth.maxHealth;
    }

    //void UpdateCooldownBar()
    //{
    //    cooldownBar.value = playerShooting.GetCooldownProgress();  // Assuming you add a method for cooldown progress
    //}
}
