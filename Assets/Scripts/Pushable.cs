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
        _slideDelay = new WaitForSeconds(stepDelay);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Only the player can push the block
        if (!other.gameObject.CompareTag("Player")) return;
        if (pushesLeft <= 0) return;

        var contacts = new ContactPoint2D[1];
        pushCollider.GetContacts(contacts);

        var playerDirection = contacts[0].normal;

        var offset = playerDirection * 1f; 

        Debug.Log($"Offset: {offset}");

        if (_isMoving == false)
        {
            _isMoving = true;
            pushesLeft--;
            StartCoroutine(nameof(Slide), offset);
        }
    }

    // Slowly move the slide the block towards the goal in the correct direction
    private IEnumerator Slide(Vector2 offset)
    {
        var alpha = 0f; 

        while (alpha < 1f) 
        {
            alpha += lerpStep;
            transform.position = Vector2.Lerp(_startPosition, _startPosition + offset, alpha);
            yield return _slideDelay;
        }

        _isMoving = false;
    }
}
