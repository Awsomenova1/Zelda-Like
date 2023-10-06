using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHitBox : MonoBehaviour
{
    [SerializeField]
    int _damage = 4;
    int _damageToPlayer = 2;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            other.GetComponent<GenericEnemy>().takeDamage(_damage);
        }
        else if(other.CompareTag("Breakable")){
            other.GetComponent<Breakable>().takeDamage(_damage);
        }
        else if(other.CompareTag("Player")){
            other.GetComponent<PlayerStatistics>().takeDamage(_damageToPlayer);
        }
        else if(other.CompareTag("Bombable")){
        //    other.GetComponent<Bombable>().takeDamage(_damage);
        }
    }
}
