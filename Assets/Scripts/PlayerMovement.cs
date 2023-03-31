using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//allows the player to move as well as take a number of other actions like attacking
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
    private float _swordTimeSeconds = .25f;//set to the length of the sword swing animation

    // Update is called once per frame
    void Update()
    {
        if(!_stats.getInAnimation()){
            playerRB.velocity = new Vector2(_horizontalDirection * _horizontalSpeed, _verticalDirection * _verticalSpeed);
            WalkAnimation();
        }
        //if the player enters an animation, halt their movement but maintain same movement direction until after animation
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
        //this can only activate if the player is not taking an action, like anothe sword swing
        if(!_stats.getInAnimation() && context.started){
            _stats.setInAnimation(true);
            swordHitboxes[_stats.getDirection()].SetActive(true);
            _animator.SetBool("Attacking", true);

            StartCoroutine(WaitForSword(_swordTimeSeconds));
        }
    }

    //ends the sword attack animation
    //another action cannot be engaged until this function finishes running
    private IEnumerator WaitForSword(float waitTime){
        yield return new WaitForSeconds(waitTime);

        _stats.setInAnimation(false);
        swordHitboxes[_stats.getDirection()].SetActive(false);
        _animator.SetBool("Attacking", false);
    }

}

