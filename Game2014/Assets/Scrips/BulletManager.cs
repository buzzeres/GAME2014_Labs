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

    // Updated GetBullet() method to take bullet type, position, and direction
    public GameObject GetBullet(BulletType bulletType, Vector3 position, Vector3 direction)
    {
        List<GameObject> bulletPool = bulletType == BulletType.PLAYER ? _playerBulletPool : _enemyBulletPool;

        foreach (var bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.transform.position = position;
                bullet.transform.up = direction; // Set the bullet's direction
                return bullet;
            }
        }

        // If no bullets are available, instantiate a new one (optional)
        GameObject newBullet = bulletType == BulletType.PLAYER
            ? Instantiate(_playerBulletPrefab)
            : Instantiate(_enemyBulletPrefab);

        newBullet.transform.position = position;
        newBullet.transform.up = direction;
        bulletPool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false); // Deactivate the bullet
    }
}
