using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    [SerializeField]
    private GameObject _playerBulletPrefab; // Player bullet prefab
    [SerializeField]
    private GameObject _enemyBulletPrefab;  // Enemy bullet prefab

    private List<GameObject> _playerBulletPool = new List<GameObject>();
    private List<GameObject> _enemyBulletPool = new List<GameObject>();

    [SerializeField]
    private int _poolSize = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            // Initialize player bullet pool
            GameObject playerBullet = Instantiate(_playerBulletPrefab);
            playerBullet.SetActive(false);
            _playerBulletPool.Add(playerBullet);

            // Initialize enemy bullet pool
            GameObject enemyBullet = Instantiate(_enemyBulletPrefab);
            enemyBullet.SetActive(false);
            _enemyBulletPool.Add(enemyBullet);
        }
    }

    // Updated Bullet to take bullet type, position, and direction
    public GameObject GetBullet(BulletType bulletType, Vector3 position, Vector3 direction)
    {
        // Use the corresponding bullet pool (Player or Enemy)
        List<GameObject> bulletPool = bulletType == BulletType.PLAYER ? _playerBulletPool : _enemyBulletPool;

        // Check for an inactive bullet in the pool
        foreach (var bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                // Reuse this bullet: activate it, set position and direction
                bullet.SetActive(true);
                bullet.transform.position = position;
                bullet.transform.up = direction;
                return bullet;  // Return the reused bullet
            }
        }

        // If no inactive bullets are found, instantiate a new one
        GameObject newBullet = bulletType == BulletType.PLAYER
            ? Instantiate(_playerBulletPrefab)
            : Instantiate(_enemyBulletPrefab);

        // Set its position and direction
        newBullet.transform.position = position;
        newBullet.transform.up = direction;

        // Add the new bullet to the pool for future reuse
        bulletPool.Add(newBullet);

        return newBullet;  // Return the newly created bullet
    }


    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false); // Deactivate the bullet
    }
}
