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

    [SerializeField]
    private Transform _shootingPoint;

    [SerializeField]
    private float _shootingCooldownTime = 0.5f;

    private BulletManager _bulletManager;
    private bool _isAlive = true;

    private void Start()
    {
        _bulletManager = BulletManager.Instance;
        StartCoroutine(ShootingRoutine());
    }

    private void Update()
    {
        if (!_isAlive) return;

        float axisX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float axisY = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;

        transform.position += new Vector3(axisX, axisY, 0);
        CheckBoundaries();
    }

    private void CheckBoundaries()
    {
        if (transform.position.x < _horizontalBoundary.min)
            transform.position = new Vector3(_horizontalBoundary.min, transform.position.y, 0);
        else if (transform.position.x > _horizontalBoundary.max)
            transform.position = new Vector3(_horizontalBoundary.max, transform.position.y, 0);

        if (transform.position.y < _verticalBoundary.min)
            transform.position = new Vector3(transform.position.x, _verticalBoundary.min, 0);
        else if (transform.position.y > _verticalBoundary.max)
            transform.position = new Vector3(transform.position.x, _verticalBoundary.max, 0);
    }

    private IEnumerator ShootingRoutine()
    {
        while (_isAlive)
        {
            _bulletManager.GetBullet(BulletType.PLAYER, _shootingPoint.position, _shootingPoint.up);
            yield return new WaitForSeconds(_shootingCooldownTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug log to check what collided
        Debug.Log("Trigger entered with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Enemy bullet hit the player");

            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager instance is NULL!");
            }
            else
            {
                Debug.Log("Calling PlayerHit method...");
                GameManager.Instance.PlayerHit(1);  // Call PlayerHit with damage 1
            }

            BulletManager.Instance.ReturnBullet(collision.gameObject);

            if (GameManager.Instance.playerHealth <= 0)
            {
                PlayerDied();
            }
        }

    }


    public void PlayerDied()
    {
        _isAlive = false;
        Debug.Log("Player Died!");
        GameManager.Instance.GameOver();  // Call GameOver when player dies
    }
}
