using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Create projectile prefab
Link prefab to "projectile" and "arrow" 
Use tranform.position to make prefab move down
Wait 1 second
Delete prefab
Remake and repeat every 5 seconds
*/
public class Sentry : MonoBehaviour// Sentry Class
{
    public ProjectileFlight ProjectPrefab;
    public Transform Launcher; 
    private float fireRate = 1f;
    private float TimeBetween;
    Vector3 launcherPos;
    float launcherX;
    float launcherY;

    void Update()
    {
        //Use this to update the Launcher's position depending on rotation
        launcherX = transform.position.x * Mathf.Sin(transform.rotation.z);
        launcherY = transform.position.y * Mathf.Sin(transform.rotation.z);//Causes Sentry to fly off
        launcherPos = new Vector3(launcherX, launcherY, transform.position.x);

        Launcher.transform.position = launcherPos;

        TimeBetween += Time.deltaTime;
        if (TimeBetween >= fireRate)//Time delay between fires
        {
            TimeBetween = 0;
            FireProj();
         }
    }
    void FireProj(){    
        Instantiate(ProjectPrefab, Launcher.position, transform.rotation);//Create a new projectile prefab
        
    }

}


