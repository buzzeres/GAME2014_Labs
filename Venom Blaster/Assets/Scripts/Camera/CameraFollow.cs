using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player’s transform
    public Vector3 offset;    // The offset between the camera and the player

    private bool isPlayerAlive = true;

    private void Start()
    {
        if (player != null)
        {
            // Calculate the offset between the camera and the player
            offset = transform.position - player.position;
            transform.position = player.position + offset;
        }
    }

    private void LateUpdate()
    {
        if (player != null && isPlayerAlive)
        {
            // Set the camera's position based on the player's position plus the offset
            transform.position = player.position + offset;
        }
    }

    public void SetPlayerDead()
    {
        isPlayerAlive = false;
        player = null;  // Clear the player reference
    }
}
