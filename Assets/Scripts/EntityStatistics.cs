using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatistics: MonoBehaviour
{
    [SerializeField]
    protected int _maxHealth;
    [SerializeField]
    protected int _currHealth;
    public enum Directions
    {
        down,
        up,
        left,
        right
    }
    protected int _facingDirection;
    protected bool _inAnimation;

    public EntityStatistics(){
        _maxHealth = 10;
        _currHealth = 10;
        _facingDirection = (int) Directions.down;
        _inAnimation = false;
    }

    public virtual void takeDamage(int damage){
        _currHealth -= damage;
        if(_currHealth <= 0){
            _currHealth = 0;
        }
    }
    public virtual void healDamage(int heal){
        _currHealth += heal;
        if(_currHealth <= _maxHealth){
            _currHealth = _maxHealth;
        }
    }
    public int getCurrentHealth(){
        return _currHealth;
    }

    public virtual void addHeart(){
        _maxHealth += 2;
        _currHealth = _maxHealth;
    }
    public virtual void setMaxHealth(int newMax){
        _maxHealth = newMax;
    }
    public int getMaxHealth(){
        return _maxHealth;
    }

    public void setDirection(int direciton){
        _facingDirection = direciton;
    }
    public int getDirection(){
        return _facingDirection;
    }
    public void decideDirection(float horizontalSpeed, float verticalSpeed){
        if(Mathf.Abs(verticalSpeed) >= Mathf.Abs(horizontalSpeed)){
            if(verticalSpeed < 0){
                _facingDirection = (int) Directions.down;
            }
            else if(verticalSpeed > 0){
                _facingDirection = (int) Directions.up;
            }
        }
        else{
            //TODO check if this faces the correct direction
            if(horizontalSpeed <= 0){
                _facingDirection = (int) Directions.left;
            }
            else{
                _facingDirection = (int) Directions.right;
            }
        }
    }

    public void setInAnimation(bool inAnimation){
        _inAnimation = inAnimation;
    }
    public bool getInAnimation(){
        return _inAnimation;
    }
}