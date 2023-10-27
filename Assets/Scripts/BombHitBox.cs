using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHitBox : MonoBehaviour
{
    [SerializeField]
    int _damage = 12;
    [SerializeField]
    int _damageToPlayer = 1;

    //private float _waitTimeSeconds = .25f;//time until explosion disapears (how long hitbox lingers)

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //StartCoroutine(WaitToDestroy(_waitTimeSeconds));
    }

    private IEnumerator WaitToDestroy(float waitTime){
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }

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
            other.GetComponent<Bombable>().takeDamage(_damage);
        }
    }
}
