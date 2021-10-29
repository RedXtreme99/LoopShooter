using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int _damage { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.DamagePlayer(_damage);
        }
        Destroy(this.gameObject);
    }
}
