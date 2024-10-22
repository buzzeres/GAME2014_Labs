using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float rotationSpeed = 5f;  // Rotation speed of the camera
    private Vector3 offset;

    private bool isPlayerAlive = true;

    private void Start()
    {
        if (player != null)
        {
            // Calculate the initial offset between the camera and the player
            offset = transform.position - player.position;
            transform.position = player.position + offset;
        }
    }

    private void LateUpdate()
    {
        if (player != null && isPlayerAlive)
        {
            // Rotate the camera around the player based on mouse movement
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.RotateAround(player.position, Vector3.up, horizontalInput);

            // Update camera position based on player's position and the offset
            transform.position = player.position + offset;
            transform.LookAt(player);  // Keep the camera looking at the player
        }
        else
        {
            Debug.Log("Player is dead or missing.");
        }
    }

    public void SetPlayerDead()
    {
        isPlayerAlive = false;
    }
}
