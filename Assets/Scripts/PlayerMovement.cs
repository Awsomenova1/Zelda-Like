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
    private WaitForSeconds _blockPushDelay = new WaitForSeconds(0.5f);
    private int _pushDirection = -1; 
    
    [SerializeField]
    private GameObject _bombPrefab;
    public bool bombUnlocked = true;

    // void Start()
    // {
    //     var dialogue = GameObject.Find("Canvas");
    //     _npcDialogue = dialogue.transform.GetChild(1).GetComponent<NPCDialogue>();
    // }
    void OnEnable() {
        _stats.PlayerDied.AddListener(OnPlayerDied);
    }

    void OnDisable() {
        _stats.PlayerDied.RemoveAllListeners();
    }

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

    private void OnPlayerDied() {
        _animator.SetTrigger("Dead");        
        StartCoroutine(nameof(StopGame));
    }

    private IEnumerator StopGame() {
        yield return new WaitForSeconds(1f);
        _animator.ResetTrigger("Dead");
        Time.timeScale = 0f;
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

        if (_animator.GetBool("Pushing") && (!IsMoving() || (direction != _pushDirection))) {
            _animator.SetBool("Pushing", false);
            _pushDirection = -1;
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
    
    private IEnumerator WaitToStartPushing(Collider2D other) {
        yield return new WaitForSeconds(0.2f);
        if (_collider.IsTouching(other) && IsMoving()) {
            _pushDirection = _stats.getDirection();
            _animator.SetBool("Pushing", true);
        }
    }

    // True if the player is moving in a direction
    private bool IsMoving() => _horizontalDirection != 0 || _verticalDirection != 0;

    private void OnCollisionEnter2D(Collision2D other) {
        if (IsMoving() && _animator.GetBool("Pushing") == false) {
            // Get a contact point
            var contacts = new ContactPoint2D[1];
            _collider.GetContacts(contacts);
            
            // Find the normalized direction vector of the object relative to the player
            Vector2Int otherObjectDir = Vector2Int.RoundToInt(contacts[0].normal);

            // Only do the push animation if the player is facing the object touched
            if (((otherObjectDir.x + _horizontalDirection) == 0) || ((otherObjectDir.y + _verticalDirection) == 0))
            {
                StartCoroutine(nameof(WaitToStartPushing), other.collider);
            }         
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (_animator.GetBool("Pushing") == true) {
            _animator.SetBool("Pushing", false);
            _pushDirection = -1;
        }
    }

    public void Bomb(InputAction.CallbackContext context){
        if(!_stats.getInAnimation() && context.started && bombUnlocked){
            GameObject bomb;
            Vector3 location;

            //place the bomb in front of the player, different spot depending on direction the player is facing
            int faceDir = _stats.getDirection();
            //facing down
            if(faceDir == 0){
                location = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            }
            //facing up
            else if(faceDir == 1){
                location = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }
            //facing left
            else if(faceDir == 2){
                location = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            }
            //facing right
            else{
                location = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            }

            bomb = Instantiate(_bombPrefab, location, Quaternion.identity);
            bomb.gameObject.SetActive(true);
        }
    }

}
