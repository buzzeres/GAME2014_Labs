using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    private void Update()
    {
        // Move the bullet
        transform.position += transform.up * _speed * Time.deltaTime;

        // If bullet goes out of screen bounds, return it to the pool
        if (transform.position.y > 10 || transform.position.y < -10)
        {
            BulletManager.Instance.ReturnBullet(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy enemy
            BulletManager.Instance.ReturnBullet(this.gameObject); // Return bullet to pool
        }
    }
}
