using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private Boundary _boundary;
    [SerializeField]
    private Vector3 _spawnPos;

    private Vector3 _direction = Vector3.down;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;

        if (transform.position.y < _boundary.min)
        {
            transform.position = _spawnPos;
        }
    }
}
