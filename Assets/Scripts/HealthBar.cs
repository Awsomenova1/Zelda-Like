using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerStatistics _stats;
    
    [Header("Prefabs")] 
    [SerializeField] private GameObject _fullHeartPrefab;
    [SerializeField] private GameObject _halfHeartPrefab;
    [SerializeField] private GameObject _emptyHeartPrefab;
    
    private int _maxHearts;
    private int _health;
    
    private void OnEnable()
    {
        // Subscribe to player health change events
        _stats.HealthChanged.AddListener(OnHealthChange);
        _stats.MaxHealthChanged.AddListener(OnMaxHealthChange);
        
        // Display the current player health on the UI
        _health = _stats.getCurrentHealth();
        _maxHearts = _stats.getMaxHealth() / 2;
        RedrawHearts();
    }

    private void OnDisable()
    {
        // Unsubscribe from player health change events
        _stats.HealthChanged.RemoveListener(OnHealthChange);
        _stats.MaxHealthChanged.RemoveListener(OnMaxHealthChange);
    }
    
    /// <summary>
    /// Fires when the player's health is changed
    /// </summary>
    /// <param name="newHealth">The new value of the player's health</param>
    private void OnHealthChange(int newHealth)
    {
        _health = newHealth;
        RedrawHearts();
    }
    
    /// <summary>
    /// Fires when the player's maximum amount of health is changed
    /// </summary>
    /// <param name="maxHealth">The maximum number of health points the player can have</param>
    private void OnMaxHealthChange(int maxHealth)
    {
        // One heart is two health points 
        _maxHearts = maxHealth / 2;
        RedrawHearts();
    }
    
    /// <summary>
    /// Remove all hearts from the hearts container UI element 
    /// </summary>
    private void ClearHearts()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    /// <summary>
    /// Redraws the hearts in the heart container to show the correct amount of health
    /// </summary>
    private void RedrawHearts()
    {
        ClearHearts();
        
        // 1 if there is a half heart; 0 otherwise
        var halfHearts = _health % 2;
        // Every 2 health points is a full heart
        var fullHearts = (_health - halfHearts) / 2;
        // Remaining hearts (not full or half hearts) 
        var emptyHearts = _maxHearts - (halfHearts + fullHearts);
       
        // Add each type of heart to the UI in the correct order
        // Full, half, then empty hearts
        for (var i = 0; i < fullHearts; i++)
        {
            AddHeart(_fullHeartPrefab);
        }

        if (halfHearts > 0)
        {
            AddHeart(_halfHeartPrefab);
        }

        for (var i = 0; i < emptyHearts; i++)
        {
            AddHeart(_emptyHeartPrefab);
        }

    }
    
    /// <summary>
    /// Adds a heart to the heart container UI element
    /// </summary>
    /// <param name="heartPrefab">A prefab containing an image component with the heart sprite</param>
    private void AddHeart(GameObject heartPrefab)
    { 
        var p = Instantiate(heartPrefab, transform, true);
        // Prevents the image from displaying at the wrong size
        p.transform.localScale = new Vector3(1, 1, 1);
    }
}
