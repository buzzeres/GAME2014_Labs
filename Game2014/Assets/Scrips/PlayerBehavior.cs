using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Boundary _horizontalBoundary;
    [SerializeField]
    private Boundary _verticalBoundary;
    private bool isAlive = true; // Check if the player is alive

    void Update()
    {
        if (!isAlive) return; // If player is dead, stop updating

        float axisX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float axisY = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;

        transform.position += new Vector3(axisX, axisY, 0);

        // Horizontal Boundary Check
        if (transform.position.x < _horizontalBoundary.min)
            transform.position = new Vector3(_horizontalBoundary.min, transform.position.y, 0);
        else if (transform.position.x > _horizontalBoundary.max)
            transform.position = new Vector3(_horizontalBoundary.max, transform.position.y, 0);

        // Vertical Boundary Check
        if (transform.position.y < _verticalBoundary.min)
            transform.position = new Vector3(transform.position.x, _verticalBoundary.min, 0);
        else if (transform.position.y > _verticalBoundary.max)
            transform.position = new Vector3(transform.position.x, _verticalBoundary.max, 0);
    }

    public void PlayerDied()
    {
        Debug.Log("Player Died!");
        isAlive = false;
    }
}
