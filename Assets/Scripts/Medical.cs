using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Medical : MonoBehaviour
{
    public float healingAmount;
    public float healingTime;   //Amount of time between health distributions

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("healthDistribution", 0f, healingTime);
    }

    //Find adjacent towers and distribute health to them at healingTime intervals
    public void healthDistribution()
    { 
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        GameObject[] slums = GameObject.FindGameObjectsWithTag("Slum");
        towers = towers.Concat(slums).ToArray();
        Vector3Int medPosition = gameObject.GetComponent<Tower>().cell;

        foreach (GameObject tower in towers)
        {
            var adjPosition = tower.GetComponent<Tower>().cell;        
            var health = tower.GetComponent<Health>().health;
            var maxHealth = tower.GetComponent<Health>().maxHealth;          
            if (tower != gameObject)
            {
                if (adjPosition.x == medPosition.x && ((adjPosition.y == medPosition.y-1) || (adjPosition.y == medPosition.y+1)))
                {
                    if(health + healingAmount < maxHealth)
                    {
                        tower.GetComponent<Health>().health += healingAmount; 
                    }
                    else
                    {
                        tower.GetComponent<Health>().health = maxHealth;
                    }
                }
        
                if (adjPosition.y == medPosition.y && (adjPosition.x == medPosition.x-1 || adjPosition.x == medPosition.x+1))
                {
                    if (health + healingAmount < maxHealth)
                    {
                        tower.GetComponent<Health>().health += healingAmount;
                    }
                    else
                    {
                        tower.GetComponent<Health>().health = maxHealth;
                    }
                }
             
                if (adjPosition.x == medPosition.x-1 && (adjPosition.y == medPosition.y-1 || adjPosition.y == medPosition.y + 1))
                {
                    if (health + healingAmount < maxHealth)
                    {
                        tower.GetComponent<Health>().health += healingAmount;
                    }
                    else
                    {
                        tower.GetComponent<Health>().health = maxHealth;
                    }
                }
            }
        }
    }

    public void changeActive(bool state)
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        GameObject[] slums = GameObject.FindGameObjectsWithTag("Slum");
        towers = towers.Concat(slums).ToArray();       
        Vector3Int medPosition = gameObject.GetComponent<Tower>().cell;
        foreach (GameObject tower in towers)
        {
            var adjPosition = tower.GetComponent<Tower>().cell;
            if (tower != gameObject)
            {
                if (adjPosition.x == medPosition.x && ((adjPosition.y == medPosition.y - 1) || (adjPosition.y == medPosition.y + 1)))
                {
                    tower.GetComponent<Tower>().OverrideTargetting = state;
                }

                if (adjPosition.y == medPosition.y && (adjPosition.x == medPosition.x - 1 || adjPosition.x == medPosition.x + 1))
                {
                    tower.GetComponent<Tower>().OverrideTargetting = state;
                }

                if (adjPosition.x == medPosition.x - 1 && (adjPosition.y == medPosition.y - 1 || adjPosition.y == medPosition.y + 1))
                {
                    tower.GetComponent<Tower>().OverrideTargetting = state;
                }
            }
        }
    }
}
