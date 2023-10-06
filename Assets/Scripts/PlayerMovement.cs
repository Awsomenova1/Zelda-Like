using System;
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
    private bool _freezeInput = false;
    //private bool _isFacingRight;
    //private bool _isFacingDown;
    [SerializeField]
    private PlayerStatistics _stats;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private BoxCollider2D _collider;

    private IEnumerator _swordCoroutine;
    private float _swordTimeSeconds = .25f;//set to the length of the sword swing animation

    // void Start()
    // {
    //     var dialogue = GameObject.Find("Canvas");
    //     _npcDialogue = dialogue.transform.GetChild(1).GetComponent<NPCDialogue>();
    // }

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
        if (_freezeInput) return;

        _horizontalDirection = context.ReadValue<Vector2>().x;
        _verticalDirection = context.ReadValue<Vector2>().y;
    }

    //determines what direction the character is walking
    private void WalkAnimation(){
        _stats.decideDirection(_horizontalDirection, _verticalDirection);
        int direction = _stats.getDirection();
        _animator.SetFloat("Direction", direction);

        if(Mathf.Abs(_horizontalDirection) > 0 || Mathf.Abs(_verticalDirection) > 0){
            _animator.SetBool("Walking", true);
        }
        else{
            _animator.SetBool("Walking", false);
        }
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
    
    // this coroutine makes sure that the player is
    // still touching a block before continuing
    private IEnumerator WaitToStartPushing(Collision2D other) {
        // yield return new WaitForSeconds(0.5f);
        if (_collider.IsTouching(other.collider)) {
            _animator.SetBool("Pushing", true);
            _freezeInput = true;
            var alpha = 0f; 
            Vector2 startPosition = transform.position;
            while (alpha < 1f) 
            {
                alpha += 0.05f;
                transform.position = Vector2.Lerp(startPosition, startPosition + new Vector2(_horizontalDirection, _verticalDirection), alpha);
                yield return new WaitForSeconds(0.05f);
            }
            _freezeInput = false;
            _animator.SetBool("Pushing", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Pushable") && (_animator.GetBool("Pushing") == false)) {
            StartCoroutine(nameof(WaitToStartPushing), other);
        }
    }

    // private void OnCollisionExit2D(Collision2D other) {
    //     if (_animator.GetBool("Pushing")) {
    //         _animator.SetBool("Pushing", false);
    //     }
    // }
}

