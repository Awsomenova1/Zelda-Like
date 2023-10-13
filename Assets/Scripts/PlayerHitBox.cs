using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField]
    int _damage = 4;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            other.GetComponent<GenericEnemy>().takeDamage(_damage);
        }
        else if(other.CompareTag("Breakable")){
            other.GetComponent<Breakable>().takeDamage(_damage);
        }
    }
    
}
