using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField] private Boundry h;
    [SerializeField] private Boundry v;

    private Vector2 _spawnPos;

    private Vector3 _direction = Vector3.down;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y, 0);
        transform.Translate(dir * _speed * Time.deltaTime);

        if (transform.position.y > v.max)
        {
            transform.position = new Vector3(transform.position.x, v.max, transform.position.z);
        }
        else if (transform.position.y < v.min)
        {
            transform.position = new Vector3(transform.position.x, v.min, transform.position.z);
        }
    }
}

