using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.Heal(20);
            Destroy(this.gameObject);
        }
    }
}
