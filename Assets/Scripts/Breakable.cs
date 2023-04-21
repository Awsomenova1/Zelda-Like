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
            broken = true;
            _animator.SetBool("Broken", broken);
            collider.isTrigger = broken;
        //}
    }
}
