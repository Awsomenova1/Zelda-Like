using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    //would giving breakable objects health make sense? should breakables be a type of enemy?
    //[SerializeField]
    //private EntityStatistics _stats;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Collider2D collider;
    [SerializeField]
    private GameObject heartPickupPrefab;

    private const float _dropChance = 0.2f;

    private bool broken;
    // Start is called before the first frame update
    void Start()
    {
        broken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage){
        //_stats.takeDamage(damage);
        //if(_stats.getCurrentHealth() == 0){
        //
            // If the roll is lucky, the pot will drop a heart 
            var roll = Random.Range(0f, 1f);
            if ((roll <= _dropChance) && (broken == false))
            {
                Instantiate(heartPickupPrefab, transform.position, Quaternion.identity);
            }

            broken = true;
            _animator.SetBool("Broken", broken);
            collider.isTrigger = broken;

        //}
    }
}
