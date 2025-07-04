using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterController : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int maxBullets = 10;
    public float fireRate = 5f;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float bulletMaxDistance = 50f;

    private float nextFireTime = 0f;
    private Queue<Bullet> bullets = new Queue<Bullet>();

    void Start()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            Bullet b = Instantiate(bulletPrefab);
            b.gameObject.SetActive(false);
            bullets.Enqueue(b);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        MoveActiveBullets();
    }

    void Shoot()
    {
        if (bullets.Count == 0)
        {
            Debug.LogWarning("Nessun proiettile disponibile");
            return;
        }

        Bullet b = bullets.Dequeue();
        b.transform.position = firePoint.position;
        b.transform.rotation = firePoint.rotation;
        b.gameObject.SetActive(true);

    }

    void MoveActiveBullets()
    {
        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            if (bullet.gameObject.activeSelf)
            {
                if (Vector3.Distance(transform.position, bullet.transform.position) > bulletMaxDistance)
                {
                    ReturnBulletToPool(bullet);
                }
            }
        }
    }

    public void ReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullets.Enqueue(bullet);
    }
}
