using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        LifeController life = other.gameObject.GetComponent<LifeController>();
        if(life != null)
        {
            int damage = 1;
            life.AddHp(-damage);
            Debug.Log($"danno{damage}");

        }
    }
}
