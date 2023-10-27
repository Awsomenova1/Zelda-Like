using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    private float _waitTimeSeconds = 3.0f;//how long until 
    private float _explodeTimeSeconds = .25f;//how long the explosion exists for 

    public GameObject explosionBox;

    public SpriteRenderer bombSprite;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToExplode(_waitTimeSeconds));
    }

    //brings up explosion hitbox after a short wait time
    private IEnumerator WaitToExplode(float waitTime){
        yield return new WaitForSeconds(waitTime);

        explosionBox.SetActive(true);
        bombSprite.enabled = false;

        StartCoroutine(WaitToDestroy(_explodeTimeSeconds));
    }

    //deletes the bomb after explosion has lasted long enough
    private IEnumerator WaitToDestroy(float waitTime){
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }

}
