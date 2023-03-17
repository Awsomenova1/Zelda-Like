using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRB;
    
    public GameObject[] swordHitboxes;

    private float _horizontalDirection;
    private float _verticalDirection;
    private float _horizontalSpeed = 8f;
    private float _verticalSpeed = 4f;
    //private bool _isFacingRight;
    //private bool _isFacingDown;
    [SerializeField]
    private PlayerStatistics _stats;
    [SerializeField]
    private Animator _animator;

    private IEnumerator _swordCoroutine;
    private float _swordTimeSeconds = .25f;//set to the length of the sword swing animation, I think

    // Update is called once per frame
    void Update()
    {
        if(!_stats.getInAnimation()){
            playerRB.velocity = new Vector2(_horizontalDirection * _horizontalSpeed, _verticalDirection * _verticalSpeed);
            WalkAnimation();
        }
        else{
            playerRB.velocity = Vector2.zero;
        }

    }

    //determines what direction the player is trying to move the character in
    public void Move(InputAction.CallbackContext context){    
        _horizontalDirection = context.ReadValue<Vector2>().x;
        _verticalDirection = context.ReadValue<Vector2>().y;
    }

    //determines what direction the character is walking
    private void WalkAnimation(){
        _stats.decideDirection(_horizontalDirection, _verticalDirection);
        int direction = _stats.getDirection();
        _animator.SetFloat("Direction", direction);
    }

    //determines when the player character can attack, and in what direction
    public void Attack(InputAction.CallbackContext context){
        //TODO possibly replace this when proper animations are implememnted, tie this functionality to unity animator
        if(!_stats.getInAnimation()){
            _stats.setInAnimation(true);
            swordHitboxes[_stats.getDirection()].SetActive(true);
            _animator.SetBool("Attacking", true);

            StartCoroutine(WaitForSword(_swordTimeSeconds));
        }
    }

    private IEnumerator WaitForSword(float waitTime){
        yield return new WaitForSeconds(waitTime);

        _stats.setInAnimation(false);
        swordHitboxes[_stats.getDirection()].SetActive(false);
        _animator.SetBool("Attacking", false);
    }

}

