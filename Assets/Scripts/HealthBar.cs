using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public PlayerStatistics stats;
    
    [Header("Prefabs")] 
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
    public GameObject emptyHeartPrefab;
    
    [Header("Health Stats")]
    [SerializeField]
    [Tooltip("The maximum number of full hearts the player can have.")]
    private int _maxHearts;
    private int _maxHealth;
    [SerializeField]
    [Tooltip("One unit of health is half a heart.")]
    [Range(0, 20)]
    private int _health;

    private void OnEnable()
    {
        _health = stats.getCurrentHealth();
        _maxHealth = stats.getMaxHealth();
        CalculateHearts();
        stats.HealthChanged.AddListener(OnHealthChange);
        stats.MaxHealthChanged.AddListener(OnMaxHealthChange);
    }

    private void OnDisable()
    {
        stats.HealthChanged.RemoveListener(OnHealthChange);
        stats.MaxHealthChanged.RemoveListener(OnMaxHealthChange);
    }

    private void OnHealthChange(int health)
    {
        _health = health;
        CalculateHearts();
    }

    private void OnMaxHealthChange(int maxHealth)
    {
        _maxHearts = maxHealth / 2;
        CalculateHearts();
    }

    private void ClearHearts()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void CalculateHearts()
    {
        ClearHearts();

        _maxHealth = _maxHearts * 2;
        var halfHearts = _health % 2;
        var fullHearts = (_health - halfHearts) / 2;
        var emptyHearts = (_maxHealth / 2) - (halfHearts + fullHearts);

        for (var i = 0; i < fullHearts; i++)
        {
            AddHeart(fullHeartPrefab);
        }

        if (halfHearts > 0)
        {
            AddHeart(halfHeartPrefab);
        }

        for (var i = 0; i < emptyHearts; i++)
        {
            AddHeart(emptyHeartPrefab);
        }

    }

    private void AddHeart(GameObject heartPrefab)
    { 
        var p = Instantiate(heartPrefab, transform, true);
        p.transform.localScale = new Vector3(1, 1, 1);
    }

}
