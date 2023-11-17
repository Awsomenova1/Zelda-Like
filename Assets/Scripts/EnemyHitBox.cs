using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    int _damage = 1;

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerStatistics>().takeDamage(_damage);
            Debug.Log("Dealt 1 damage");
        }

        if(this.CompareTag("Projectile")){//Checks if item using script is a Projectile
            Destroy(gameObject);//Destroy on contact
        }

    }
}
