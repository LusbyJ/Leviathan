using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameController gameController;
    public Vector3Int cell;
    public float attackTime;
    public float damage;
    public int cost;
    public int upgradeLevel = 1;
    public int upgradeCost;
    public GameObject projectile;
    public bool OverrideTargetting=false; //Used for medical tower active

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && !Input.GetKey("left shift"))
        {
            Debug.Log("Trying to use ability " + gameObject.name);
            //do stuff
        }
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Trying to upgrade tower");
        }
    }
}
