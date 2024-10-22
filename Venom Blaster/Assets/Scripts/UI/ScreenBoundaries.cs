using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float playerWidth;
    private float playerHeight;

    void Start()
    {
        // Calculate the screen boundaries
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Get the player's width and height based on its collider
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        playerHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void Update()
    {
        // Restrict the player's position within the boundaries
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + playerWidth, screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + playerHeight, screenBounds.y - playerHeight);
        transform.position = viewPos;
    }
}
