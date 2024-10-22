using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private float _horizontalSpeed = 1.0f;

    [SerializeField]
    private Transform _shootingPoint; // Position from where the enemy shoots

    [SerializeField]
    private float _shootingCooldownTime = 1.5f; // Time between shots

    private Boundary _horizontalBoundary;
    private Boundary _verticalBoundary;

    private bool _movingRight = true;
    private BulletManager _bulletManager;

    void Start()
    {
        _bulletManager = BulletManager.Instance; // Accessing the BulletManager Singleton

        SetRandomProperties();
        transform.position = new Vector3(Random.Range(_horizontalBoundary.min, _horizontalBoundary.max), _verticalBoundary.max, 0);

        StartCoroutine(ShootingRoutine()); // Start shooting
    }

    void Update()
    {
        // Move the enemy down
        transform.position += Vector3.down * _speed * Time.deltaTime;

        // Horizontal movement
        if (_movingRight)
        {
            transform.position += Vector3.right * _horizontalSpeed * Time.deltaTime;
            if (transform.position.x > _horizontalBoundary.max)
            {
                _movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * _horizontalSpeed * Time.deltaTime;
            if (transform.position.x < _horizontalBoundary.min)
            {
                _movingRight = true;
            }
        }

        // Respawn if the enemy moves past the bottom vertical boundary
        if (transform.position.y < _verticalBoundary.min)
        {
            Respawn();
        }
    }

    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            // Get and fire an enemy bullet
            GameObject bullet = _bulletManager.GetBullet(BulletType.ENEMY, _shootingPoint.position, Vector3.down);
            yield return new WaitForSeconds(_shootingCooldownTime); // Wait for the cooldown before shooting again
        }
    }

    public void SetBoundaries(Boundary horizontal, Boundary vertical)
    {
        _horizontalBoundary = horizontal;
        _verticalBoundary = vertical;
    }

    void Respawn()
    {
        SetRandomProperties();
        transform.position = new Vector3(Random.Range(_horizontalBoundary.min, _horizontalBoundary.max), _verticalBoundary.max, 0);
    }

    private void SetRandomProperties()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        GetComponent<SpriteRenderer>().color = randomColor;

        _speed = Random.Range(1.0f, 4.0f);
        _horizontalSpeed = Random.Range(0.5f, 2.5f);
    }
}
