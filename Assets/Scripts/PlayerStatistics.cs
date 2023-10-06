using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerStatistics : EntityStatistics
{
    //private bool _bombUnlocked;

    //determines if player is invincible
    //set temporarily after getting hit by enemy so that player can't take too much damage at once
    private bool _invulnerable = false;
    private float _invulTimeSeconds = 1.5f;
    [HideInInspector] public UnityEvent<int> HealthChanged;
    [HideInInspector] public UnityEvent<int> MaxHealthChanged;

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
        if(!_invulnerable){
            base.takeDamage(damage);
            HealthChanged?.Invoke(_currHealth);
            StartCoroutine(WaitForInvulnerable(_invulTimeSeconds));
        }
    }

    private IEnumerator WaitForInvulnerable(float waitTime){
        _invulnerable = true;
        yield return new WaitForSeconds(waitTime);
        _invulnerable = false;
    }
}