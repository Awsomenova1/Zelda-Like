using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileFlight : MonoBehaviour// Sentry Class
{
    [SerializeField] float speed = 5f;
    Quaternion projRotation;
    Vector3 currEuler;
    int z;
    [SerializeField] private GameObject sentry;
    //[SerializeField] private Transform Launcher;
    void Start(){
        //Updates to the rotation to match sentry
        z = (int)sentry.transform.eulerAngles.z + 83;
    }   
    

    void Update()
    {   
        currEuler = new Vector3(0, 0, z);// * Time.deltaTime * speed;
        projRotation.eulerAngles = currEuler;
        transform.rotation = projRotation;
        
        transform.position += transform.up * Time.deltaTime * speed;
        
    }
    

}
