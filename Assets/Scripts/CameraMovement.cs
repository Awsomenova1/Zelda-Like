using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes the game camera follow the player character
public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    // Start is called before the first frame update
    void Start()
    {
        target = gameObject.transform.Find("/Player");
    }

    // LateUpdate is called once per frame
    // after all Update functions have been called 
    //LateUpdate is used here instead of Update so all changes that the camera might see will occur before the camera updates to view it
    void LateUpdate()
    {
        if(transform.position != target.position){
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
