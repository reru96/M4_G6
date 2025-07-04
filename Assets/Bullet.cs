using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            LifeController life = collision.collider.GetComponent<LifeController>();
            if (life != null)
            {
                life.AddHp(-1);
                
            }

            FindObjectOfType<PlayerShooterController>().ReturnBulletToPool(this);
        }
    }
}
