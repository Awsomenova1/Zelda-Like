using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatistics : EntityStatistics
{
    //private bool _bombUnlocked;
    [HideInInspector]
    public UnityEvent<int> HealthChanged;
    [HideInInspector]
    public UnityEvent<int> MaxHealthChanged;

    public override void addHeart()
    {
        base.addHeart();
        MaxHealthChanged?.Invoke(_currHealth);
    }

    public override void setMaxHealth(int newMax)
    {
        base.setMaxHealth(newMax);
        MaxHealthChanged?.Invoke(newMax);
    }

    public override void healDamage(int heal)
    {
        base.healDamage(heal);
        HealthChanged?.Invoke(_currHealth);
    }

    public override void takeDamage(int damage)
    {
        base.takeDamage(damage);
        HealthChanged?.Invoke(_currHealth);
    }
}