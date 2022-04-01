using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Vector3Int cell;
    public float attackTime;
    public int damage;
    public int cost;
    public GameObject projectile;
    public bool OverrideTargetting=false; //Used for medical tower active

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
