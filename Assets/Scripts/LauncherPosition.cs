using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LauncherPosition : MonoBehaviour// Sentry Class
{
    [SerializeField] private GameObject sentry;

    float x;
    float y;
    float z;

    void Start(){
        //Updates to the rotation to match sentry
        x = sentry.transform.position.x;
        y = sentry.transform.position.y;
        z = sentry.transform.position.z;
    }   
    

    void Update()
    {   
        Debug.Log("X: " + x);
        //currEuler = new Vector3(0, 0, z);// * Time.deltaTime * speed;
        //projRotation.eulerAngles = currEuler;
        //transform.rotation = projRotation;
        
        //transform.position += transform.up * Time.deltaTime * speed;
        
    }
    

}