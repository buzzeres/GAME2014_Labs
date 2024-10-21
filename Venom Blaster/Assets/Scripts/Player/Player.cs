using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : BaseCharacter
{
    public GameObject projectilePrefab;  // The projectile prefab for shooting
    public Transform firePoint;          // The point where the projectile will be instantiated
    public float fireRate = 3f;          // Cooldown time in seconds
    private float nextFireTime = 0f;

    public Joystick joystick;            // For mobile controls
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 5f;

    public Slider cooldownSlider;        // Slider to display the cooldown

    public TextMeshProUGUI timeText;     // TextMeshPro component to display the time
    public TextMeshProUGUI enemyKillText;  // TextMeshPro component to display enemy kills

    private int enemyKillCount = 0;      // To track the number of enemies killed
    private float elapsedTime = 0f;

    private bool isShootButtonPressed = false;

    public GameOverManager gameOverManager;  // Reference to the Game Over manager
    public AudioClip shootAudioClip;     // Reference to the AudioClip for the shooting sound
    private AudioSource audioSource;     // AudioSource to play the AudioClip

    protected override void Start()
    {
        base.Start();  // Calls the Start function from BaseCharacter to initialize health
        currentHealth = 100;  // Ensure player starts with 100 health
        healthSystem.UpdateHealthUI();  // Make sure the health bar starts full
        rb = GetComponent<Rigidbody2D>();

        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if missing
        }

        // Initialize the cooldown slider
        if (cooldownSlider != null)
        {
            cooldownSlider.maxValue = fireRate;  // Max value is the cooldown time
            cooldownSlider.value = 0f;           // Start with no cooldown
        }

        // Initialize the text components
        UpdateTimeText();
        UpdateEnemyKillText();
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();

        // Update the elapsed time
        elapsedTime += Time.deltaTime;
        UpdateTimeText();

        // Update the cooldown slider
        UpdateCooldownSlider();
    }

    private void HandleMovement()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        if (movement == Vector2.zero)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        if (movement.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleShooting()
    {
        if (Time.time >= nextFireTime && isShootButtonPressed)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
            isShootButtonPressed = false;
        }
    }

    public void ShootButtonPressed()
    {
        isShootButtonPressed = true;
    }

    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Play the shooting sound
            if (shootAudioClip != null)
            {
                audioSource.PlayOneShot(shootAudioClip);
            }

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = projectile.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Initialize(firePoint.up, "Enemy");  // Assuming "Enemy" is the target
            }
        }
    }

    private void UpdateCooldownSlider()
    {
        if (cooldownSlider != null)
        {
            float remainingCooldown = nextFireTime - Time.time;

            if (remainingCooldown > 0)
            {
                cooldownSlider.value = remainingCooldown;
            }
            else
            {
                cooldownSlider.value = 0;
            }
        }
    }

    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = $"Time: {elapsedTime:F2}";
        }
    }

    private void UpdateEnemyKillText()
    {
        if (enemyKillText != null)
        {
            enemyKillText.text = $"Enemies Killed: {enemyKillCount}";
        }
    }

    public void EnemyKilled()
    {
        enemyKillCount++;
        UpdateEnemyKillText();
    }

    protected override void Die()
    {
        base.Die();
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
    }
}
