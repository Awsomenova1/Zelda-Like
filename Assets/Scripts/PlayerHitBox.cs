using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField]
    int _damage = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            other.GetComponent<GenericEnemy>().takeDamage(_damage);
            //TODO add proper enemy scripts and change case to reflect breakable objects being seperate
        }
        else if(other.CompareTag("Breakable")){
            other.GetComponent<Breakable>().takeDamage(_damage);
        }
    }
    
}
