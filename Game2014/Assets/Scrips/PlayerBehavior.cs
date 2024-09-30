using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Boundary _horizontalBoundary; // Correct spelling
    [SerializeField]
    private Boundary _verticalBoundary;   // Correct spelling

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Gets input and calculates the movement
        float axisX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float axisY = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;

        transform.position += new Vector3(axisX, axisY, 0);

        // Check if player passes the boundary, if they do, switch side like pac-man
        if (transform.position.x < _horizontalBoundary.min)
        {
            transform.position = new Vector3(_horizontalBoundary.min, transform.position.y, 0);
        }
        else if (transform.position.x > _horizontalBoundary.max)
        {
            transform.position = new Vector3(_horizontalBoundary.max, transform.position.y, 0);
        }

        // Check if player passes the boundary, if they do, stop it on the edge
        if (transform.position.y < _verticalBoundary.min)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.min, 0);
        }
        else if (transform.position.y > _verticalBoundary.max)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundary.max, 0);
        }
    }
}
