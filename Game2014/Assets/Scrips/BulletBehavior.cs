using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private BulletType _bulletType;
    private BulletManager _bulletManager;

    // Screen boundaries
    private float _screenTop;
    private float _screenBottom;
    private float _screenLeft;
    private float _screenRight;

    private void Start()
    {
        _bulletManager = BulletManager.Instance; // Access the singleton instance

        // Calculate screen boundaries based on the camera's view
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint
            (new Vector3(Screen.width, Screen.height, 0));
        _screenTop = screenBounds.y;
        _screenBottom = -screenBounds.y;
        _screenLeft = -screenBounds.x;
        _screenRight = screenBounds.x;
    }

    private void Update()
    {
        // Move the bullet
        transform.position += transform.up * _speed * Time.deltaTime;

        // Check if bullet is out of screen bounds
        if (transform.position.y > _screenTop || transform.position.y < _screenBottom ||
            transform.position.x > _screenRight || transform.position.x < _screenLeft)
        {
            _bulletManager.ReturnBullet(this.gameObject); // Return bullet to the pool
        }
    }

    public void Initialize(BulletType bulletType, Vector3 position, Vector3 direction)
    {
        _bulletType = bulletType;
        transform.position = position;
        transform.up = direction; // Set the bullet's direction
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_bulletType == BulletType.PLAYER && collision.CompareTag("Enemy"))
        {
            GameManager.Instance.EnemyDestroyed(collision.gameObject);  // Update score in GameManager
            _bulletManager.ReturnBullet(this.gameObject);  // Return bullet to the pool
        }
    }
}
