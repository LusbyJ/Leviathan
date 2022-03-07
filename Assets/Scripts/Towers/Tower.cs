using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackTime;
    public int cost;
    public GameObject projectile;
  

    // Update is called once per frame
    void Update()
    {
            
    }

    //Upgrade tower
    void OnMouseDown()
    {
        if (Input.GetKey("left shift") || Input.GetKey("right shift"))
        {
            Debug.Log("Trying to upgrade " + gameObject.name);
            //do stuff
        }

    }


    //Use active ability
    void OnMouseOver()
    { 
        if(Input.GetMouseButtonDown(1) && !Input.GetKey("left shift"))
        {
            Debug.Log("Trying to use ability " + gameObject.name);
            //do stuff
        }
    }
}
