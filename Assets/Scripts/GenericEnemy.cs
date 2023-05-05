using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * genericEnemy 
 * When enemy is not spotted, enemy is "in patrol mode" and will pace between two defined points
 * If player is within visibility, enemy will go towards player location
 *      If player is hidden behind wall/boundary, player will not be seen by enemy
 *      
 *  When enemy is within contact of player, an attack timer is set. When timer runs out, enemy attacks, and player health
 *  is decreased if player is still within range. Timer is then reset.
 */

public class GenericEnemy : MonoBehaviour
{
    public Rigidbody2D enemyRB;

    [SerializeField]
    private PlayerMovement PlayerMovement;

    private float _horizontalDirection;
    private float _verticalDirection;
    private float _horizontalSpeed = 2f;
    //private float _verticalSpeed = 4f;
    private float viewDistance = 10f;
    private float viewAngle = 90f;

    private bool _isFacingRight;
    private bool _isFacingDown;

    private bool _playerSpotted;    // playerSpotted: bool flag that determines whether player is within enemy LoS
    private bool _startToEnd;       // startToEnd: bool flag that determines if enemy if moving from start to end point. If false, enemy is moving from end to start
    public Vector3 _enemyStartPos;  // enemyStartPos: When in patrol mode, enemy will start at this positions
    public Vector3 _enemyEndPos;    // enemyEndPos: When in patrol mode, enemy will end at this position, then go back to start pos

    [SerializeField]
    private EntityStatistics _stats;

    // Start is called before the first frame update
    void Start()
    {
        _playerSpotted = false;
        _startToEnd = true;
        enemyRB = GetComponent<Rigidbody2D>();
        _enemyStartPos = transform.position; // Enemy position in scripting is set to enemyStartPos
        Debug.Log("Enemy start pos.x: " + _enemyStartPos.x);

    }

    // Update is called once per frame
    void Update()
    {
        // If player is not spotted, pace back and forth between defined place on scene
        var step = _horizontalSpeed * Time.deltaTime; // calculate distance to move

        if (!_playerSpotted)
            patrol(step);
            // Functionality to check to see player:
            //Debug.Log(findThePlayer(PlayerMovement.GetTransform()));


        // If player is spotted, _playerSpotted = true

        // Attempts at trying to spot player using raycasting are as follows:
        //Debug.DrawLine(this.transform.position, this.transform.position + this.transform.right, Color.red, 2);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 20f);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 20f, Color.red);

        //if (hit)
        //{
        //    Debug.Log("Raycast hit something: " + hit.collider.name);
        //    hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
        //    //_playerSpotted = true;
        //} else if (!hit)
        //{
        //    Debug.Log("Raycast has hit nothing");
        //}


        if (_playerSpotted)
        {
            // Move towards player and attack
        }
    }

    // Patrol: Enemy continuously moves between two different points while enemy is patrolling
    void patrol(float step)
    {
        if (_startToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, _enemyEndPos, step);
            if (transform.position == _enemyEndPos)
            {
                _startToEnd = false;
            }
        }
        else if (!_startToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, _enemyStartPos, step);
            if (transform.position == _enemyStartPos)
            {
                _startToEnd = true;
            }
        }

    }

    // findThePlayer: Given player's position, calculates dist. and visibility of player from enemy's pov
    // Here you have to pass the players position by the players transform
    bool findThePlayer(Transform playerPos)
    {
        if (Vector3.Distance(transform.position, playerPos.position) < viewDistance)
        {
            Vector3 directionToPlayer = (playerPos.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2)
            {
                Debug.Log("viewAngle " + viewAngle);
                if (!Physics.Linecast(transform.position, playerPos.position))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void takeDamage(int damage){
        _stats.takeDamage(damage);
        if(_stats.getCurrentHealth() == 0){
            gameObject.SetActive(false);
        }
    }

    //void FixedUpdate()
    //{
    //    Vector3 fwd = transform.TransformDirection(Vector3.forward);

    //    if (Physics.Raycast(transform.position, fwd, 10))
    //        print("There is something in front of the object!");
    //}

} // end of class
