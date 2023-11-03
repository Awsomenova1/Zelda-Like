using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes the game camera follow the player character
public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    public Vector2 maxPosition;
    public Vector2 minPosition;

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
        //move the camera to follow the target (usually the player)
        if(transform.position != target.position){
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            //limit the camera to particular bounds
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

        }
    }
}
