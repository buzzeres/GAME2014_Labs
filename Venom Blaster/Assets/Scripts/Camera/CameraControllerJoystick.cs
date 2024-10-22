using UnityEngine;

public class CameraControllerJoystick : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // Offset between the camera and player
    public Vector3 fallbackPosition = new Vector3(0, 0, -10); // Static fallback position for the camera
    public float transitionSpeed = 2f;  // Speed for camera transition after player is destroyed

    private bool isPlayerAlive = true;

    private void LateUpdate()
    {
        if (player != null && isPlayerAlive)
        {
            // Update camera position based on player's position and the offset
            transform.position = player.position + offset;
        }
        else
        {
            // Smooth transition to fallback position after player is destroyed
            transform.position = Vector3.Lerp(transform.position, fallbackPosition, transitionSpeed * Time.deltaTime);
        }
    }

    public void SetPlayerDead()
    {
        isPlayerAlive = false;
        player = null;  // Clear the player reference
    }
}
