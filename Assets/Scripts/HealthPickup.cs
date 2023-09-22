using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] private BoxCollider2D pickupCollider;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Give the player a heart and destroy the pickup
        if (other.CompareTag("Player"))
        {
            var playerStats = other.gameObject.GetComponent<PlayerStatistics>();
            
            // Heal a full heart
            playerStats.healDamage(2);

            Destroy(gameObject);
        }
    }
}
