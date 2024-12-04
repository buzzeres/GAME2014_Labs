using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour, IDamage
{
    Rigidbody2D _rigidbody;
    [SerializeField]
    float _speed = 10;
    [SerializeField]
    int _damage = 5;

    public int Damage()
    {
        return _damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Vector3 directionToTarget = (FindAnyObjectByType<PlayerBehavior>().transform.position - transform.position).normalized;
        _rigidbody.AddForce(directionToTarget * _speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBarController healthBar = FindObjectOfType<HealthBarController>();
            if (healthBar != null)
            {
                healthBar.TakeDamage(_damage);
            }
            else
            {
                Debug.LogError("HealthBarController is not found in the scene");
            }
            Destroy(gameObject);
        }
    }
}
