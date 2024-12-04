using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    [SerializeField]
    float _speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Vector3 directionToTarget = (FindAnyObjectByType<PlayerBehavior>().transform.position - transform.position).normalized;
        _rigidbody.AddForce(directionToTarget * _speed, ForceMode2D.Impulse);
    }
}
