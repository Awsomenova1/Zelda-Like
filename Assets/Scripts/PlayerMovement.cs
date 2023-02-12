using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRB;

    private float _horizontalDirection;
    private float _verticalDirection;
    private float _horizontalSpeed = 8f;
    private float _verticalSpeed = 4f;
    private bool _isFacingRight;
    private bool _isFacingDown;
    // Update is called once per frame
    void Update()
    {
        playerRB.velocity = new Vector2(_horizontalDirection * _horizontalSpeed, _verticalDirection * _verticalSpeed);
        //rotate();
        //TODO change flip code to change used sprites
        //TODO create placeholder sprites for character
    }

    public void Move(InputAction.CallbackContext context){
        _horizontalDirection = context.ReadValue<Vector2>().x;
        _verticalDirection = context.ReadValue<Vector2>().y;
    }

    public void Attack(InputAction.CallbackContext context){
        //TODO make function to use sword attack
    }

    public void Flip(){
        //if(_verticalDirection > 0){}
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void FlipVertical(){
        _isFacingDown = !_isFacingDown;
        Vector3 localScale = transform.localScale;
        localScale.y *= -1f;
        transform.localScale = localScale;
    }

    public void rotate(){
        /*Vector3 TargetDirection = 

        if(_horizontalDirection > 0f){
            transform.RotateTowards();
        }
        else if(_horizontalDirection < 0f){
            transform.Rotate(faceLeft);
        }*/
        Vector2 moveDirection = playerRB.velocity;
        if (moveDirection != Vector2.zero) {//add conditions later to make only face cardinal directions
            float angle = Mathf.Atan2(-(moveDirection.y), -(moveDirection.x)) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    /*public void Jump(InputAction.CallbackContext context){
        if(context.performed && IsGrounded()){
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpingPower);
        }

        //second part
    }*/

}

