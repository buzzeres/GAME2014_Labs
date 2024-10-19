using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;  // Reference to the joystick
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Ensure the Player has a Rigidbody2D
    }

    void Update()
    {
        // Get the input from the joystick for movement
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        // If there's no joystick input, fall back to WASD input
        if (movement == Vector2.zero)
        {
            movement.x = Input.GetAxisRaw("Horizontal");  // A/D or Left/Right arrows
            movement.y = Input.GetAxisRaw("Vertical");    // W/S or Up/Down arrows
        }

        // Rotate the player based on movement input
        RotatePlayer();
    }

    void FixedUpdate()
    {
        // Apply the movement to the player using Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Rotate the player based on the movement input
    private void RotatePlayer()
    {
        if (movement.magnitude > 0.1f)  // Only rotate if input is significant
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));  // Rotate the player to face the movement direction
        }
    }
}
