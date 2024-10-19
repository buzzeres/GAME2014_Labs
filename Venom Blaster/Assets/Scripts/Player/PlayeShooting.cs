using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 3f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        nextFireTime = Time.time + fireRate;
    }
}
