using System;
using System.Collections;
using UnityEngine;

public class Pushable : MonoBehaviour 
{ 
    [SerializeField] private BoxCollider2D pushCollider;
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private int pushesLeft = 1;
    [SerializeField] private float pushTime = 0.5f;
    private WaitForSeconds _slideDelay;
    private Vector2 _startPosition;
    private bool _isMoving;
    private bool _stillFacingBlock = false;
    private bool _stillTouching = false;


    private void Start()
    {
        _startPosition = transform.position;
        _isMoving = false;
    }

    // Check if the player is facing in the direction of the block
    private bool PlayerFacingBlock(EntityStatistics.Directions facingDirection, Vector2Int playerDirection) {
            return (
                (facingDirection == EntityStatistics.Directions.up && playerDirection.y == 1)
                || (facingDirection == EntityStatistics.Directions.down && playerDirection.y == -1)
                || (facingDirection == EntityStatistics.Directions.left && playerDirection.x == -1)
                || (facingDirection == EntityStatistics.Directions.right && playerDirection.x == 1)
            );
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Only the player can push the block
        if (!other.gameObject.CompareTag("Player")) return; 
        if (pushesLeft <= 0) return;
        var playerStats = other.gameObject.GetComponent<PlayerStatistics>();
        var facingDirection = (EntityStatistics.Directions)playerStats.getDirection();

        // Reset the touching state
        _stillTouching = false;

        // Get the contact vector of the player 
        var contacts = new ContactPoint2D[1];
        pushCollider.GetContacts(contacts);
        
        // Get a normalized vector representing the direction the player
        // is in relative to the block
        var playerDirection = Vector2Int.RoundToInt(contacts[0].normal);
        var offset = playerDirection * 1; 

        // The block can only be pushed when it's not moving
        if (_isMoving == false)
        {
            _isMoving = true;
            StartCoroutine(Slide(other, offset));
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player")) return;

        var playerStats = other.gameObject.GetComponent<PlayerStatistics>();
        var facingDirection = (EntityStatistics.Directions)playerStats.getDirection();

        var contacts = new ContactPoint2D[1];
        pushCollider.GetContacts(contacts);

        var playerDirection = Vector2Int.RoundToInt(contacts[0].normal);

        _stillTouching = true;
        _stillFacingBlock = PlayerFacingBlock(facingDirection, playerDirection);
    }

    // Slowly move the slide the block towards the goal in the correct direction
    // Play with lerpStep and stepDelay to tune the animation 
    private IEnumerator Slide(Collision2D other, Vector2Int offset)
    {
        yield return new WaitForSeconds(1f);

        // Is the player still touching and facing the block?
        if (_stillTouching && _stillFacingBlock) {
            var time = 0f; 
            
            // Slide the block towards its new position 
            while (time < pushTime)
            {
                rb.position = Vector2.Lerp(_startPosition, _startPosition + offset, time / pushTime);
                time += Time.deltaTime;
                yield return null;
            }

            // The block has been pushed to a new position, so reset the start position
            _startPosition = transform.position;
            _isMoving = false;
            pushesLeft--;
        } else {
            _isMoving = false;
        }
    }
}
