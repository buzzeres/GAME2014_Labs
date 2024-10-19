using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro namespace

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject projectilePrefab;  // The projectile prefab
    public Transform firePoint;          // The point where the projectile will be instantiated
    public float fireRate = 3f;          // Cooldown time in seconds
    private float nextFireTime = 0f;     // Time when the player can shoot again
    public Slider healthSlider;          // Reference to the Health UI Slider
    public Slider cooldownSlider;        // Reference to the Cooldown UI Slider
    public TextMeshProUGUI enemyKillCountText;  // Use TextMeshProUGUI for displaying kill count
    private int enemyKillCount = 0;      // Track the number of enemies killed

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        cooldownSlider.maxValue = 1;
        cooldownSlider.value = 0;
        UpdateKillCountUI();  // Initialize kill count
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Time.time < nextFireTime)
        {
            cooldownSlider.value = (nextFireTime - Time.time) / fireRate;
        }
        else
        {
            cooldownSlider.value = 0;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead");
           // Destroy(gameObject);  // Destroy the player when health reaches 0
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    public void IncreaseKillCount()
    {
        enemyKillCount++;
        UpdateKillCountUI();
    }

    private void UpdateKillCountUI()
    {
        if (enemyKillCountText != null)
        {
            enemyKillCountText.text = "Enemies Killed: " + enemyKillCount;
        }
    }
}
