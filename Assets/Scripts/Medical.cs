using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Medical : MonoBehaviour
{
    public float healingAmount;
    public float healingTime;
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
        foreach (GameObject tower in towers)
        {
            if (tower != gameObject)
            {
                if (tower.GetComponent<Tower>().cell.x == gameObject.GetComponent<Tower>().cell.x && tower.GetComponent<Tower>().cell.y == gameObject.GetComponent<Tower>().cell.y-1)
                {
                    if(tower.GetComponent<Health>().health + healingAmount < tower.GetComponent<Health>().maxHealth)
                    {
                        tower.GetComponent<Health>().health += healingAmount;
                    }
                    else
                    {
                        tower.GetComponent<Health>().health = tower.GetComponent<Health>().maxHealth;
                    }
                }
                if (tower.GetComponent<Tower>().cell.x == gameObject.GetComponent<Tower>().cell.x && tower.GetComponent<Tower>().cell.y == gameObject.GetComponent<Tower>().cell.y+1)
                {
                    if (tower.GetComponent<Health>().health + healingAmount < tower.GetComponent<Health>().maxHealth)
                    {
                        tower.GetComponent<Health>().health += healingAmount;
                    }
                    else
                    {
                        tower.GetComponent<Health>().health = tower.GetComponent<Health>().maxHealth;
                    }
                }
                if (tower.GetComponent<Tower>().cell.y == gameObject.GetComponent<Tower>().cell.y && tower.GetComponent<Tower>().cell.x == gameObject.GetComponent<Tower>().cell.x-1)
                {
                    if (tower.GetComponent<Health>().health + healingAmount < tower.GetComponent<Health>().maxHealth)
                    {
                        tower.GetComponent<Health>().health += healingAmount;
                    }
                    else
                    {
                        tower.GetComponent<Health>().health = tower.GetComponent<Health>().maxHealth;
                    }
                }
                if (tower.GetComponent<Tower>().cell.y == gameObject.GetComponent<Tower>().cell.y && tower.GetComponent<Tower>().cell.x == gameObject.GetComponent<Tower>().cell.x+1)
                {
                    if (tower.GetComponent<Health>().health + healingAmount < tower.GetComponent<Health>().maxHealth)
                    {
                        tower.GetComponent<Health>().health += healingAmount;
                    }
                    else
                    {
                        tower.GetComponent<Health>().health = tower.GetComponent<Health>().maxHealth;
                    }
                }
                if (tower.GetComponent<Tower>().cell.x == gameObject.GetComponent<Tower>().cell.x-1 && tower.GetComponent<Tower>().cell.y == gameObject.GetComponent<Tower>().cell.y-1)
                {
                    if (tower.GetComponent<Health>().health + healingAmount < tower.GetComponent<Health>().maxHealth)
                    {
                        tower.GetComponent<Health>().health += healingAmount;
                    }
                    else
                    {
                        tower.GetComponent<Health>().health = tower.GetComponent<Health>().maxHealth;
                    }
                }
                if (tower.GetComponent<Tower>().cell.x == gameObject.GetComponent<Tower>().cell.x-1 && tower.GetComponent<Tower>().cell.y == gameObject.GetComponent<Tower>().cell.y+1)
                {
                    if (tower.GetComponent<Health>().health + healingAmount < tower.GetComponent<Health>().maxHealth)
                    {
                        tower.GetComponent<Health>().health += healingAmount;
                    }
                    else
                    {
                        tower.GetComponent<Health>().health = tower.GetComponent<Health>().maxHealth;
                    }
                }
            }
        }
    }
}
