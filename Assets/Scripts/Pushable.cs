using System;
using System.Collections;
using UnityEngine;

public class Pushable : MonoBehaviour 
{ 
    [SerializeField] private BoxCollider2D pushCollider;
    [SerializeField] private int pushesLeft = 1;
    [SerializeField] private float lerpStep = 0.05f;
    [SerializeField] private float stepDelay = 0.05f;
    private WaitForSeconds _slideDelay;
    private Vector2 _startPosition;
    private bool _isMoving;


    private void Start()
    {
        _startPosition = transform.position;
        _isMoving = false;
        // Cache object to improve performance
        _slideDelay = new WaitForSeconds(stepDelay);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Only the player can push the block
        if (!other.gameObject.CompareTag("Player")) return; 
        if (pushesLeft <= 0) return;

        // Get the contact vector of the player 
        var contacts = new ContactPoint2D[1];
        pushCollider.GetContacts(contacts);
        
        // Get a normalized vector representing the direction the player
        // is in relative to the block
        var playerDirection = contacts[0].normal;

        var offset = playerDirection * 1f; 

        // The block can only be pushed when
        // it's not moving
        if (_isMoving == false)
        {
            _isMoving = true;
            pushesLeft--;
            StartCoroutine(Slide(other, offset));
        }
    }

    // Slowly move the slide the block towards the goal in the correct direction
    // Play with lerpStep and stepDelay to tune the animation 
    private IEnumerator Slide(Collision2D other, Vector2 offset)
    {
        Debug.Log("waiting...");
        yield return new WaitForSeconds(1f);

        if (pushCollider.IsTouching(other.collider)) {
            Debug.Log("yes");
            var alpha = 0f; 

            while (alpha < 1f) 
            {
                alpha += lerpStep;
                transform.position = Vector2.Lerp(_startPosition, _startPosition + offset, alpha);
                yield return _slideDelay;
            }

            // the block has been pushed to a new position, so reset the start position
            _startPosition = transform.position;
            _isMoving = false;
        } else {
            Debug.Log("no");
            _isMoving = false;
        }
    }
}
