using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Boundry _horizontalBounady;
    [SerializeField]
    private Boundry _verticalBoundry;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gets input and calculates the movement
        float axisX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float axisY = Input.GetAxisRaw("Vertical")* _speed* Time.deltaTime;

        transform.Translate(axisX, axisY, 0);

        if (transform.position.x > _horizontalBounady.max)
        {
            transform.position = new Vector3(_horizontalBounady.max, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < _horizontalBounady.max)
        {
            transform.position = new Vector3(_horizontalBounady.max, transform.position.y, transform.position.z);
        }   
    }
}
