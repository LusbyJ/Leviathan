using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Vector3Int cell;
    public float attackTime;
    public float damage;
    public int cost;
    public int upgradeLevel = 1;
    public float upgradeCost;
    public GameObject projectile;
    public bool OverrideTargetting=false; //Used for medical tower active
    private bool upgrading = false;
    private bool activeAbility = false;
    public bool used = false;

    public Sprite level2Sprite;
    public Sprite level3Sprite;

    // Update is called once per frame
    void Update()
    {
        if (upgradeLevel == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = level2Sprite;
        }
        
        if(upgradeLevel == 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = level3Sprite;
            if (!used)
            {
                activeAbility = true;
            }
        }
    }

    //Detects button clicks when mouse is over tower
    void OnMouseOver()
    {
        //If right click use active ability
        if(Input.GetMouseButtonDown(1) && activeAbility && !used)
        {
            used = true;
            //TODO use active ability
        }

        //If left click check if credits are sufficient and upgrade
        if(Input.GetMouseButtonDown(0))
        {
            if(GameController.instance.credits >= upgradeCost && upgradeLevel < 3)
            {         
                GameController.instance.credits -= upgradeCost;
                upgradeLevel++;
                upgradeCost *= 2;
                upgrading = true;
                applyUpgrades();
            }      
        }
    }

    //Apply tower specific upgrades then reset upgrading bool
    public void applyUpgrades()
    {
        //Upgrade Gunner
        if(gameObject.name == "Gunner(Clone)")
        {
            gameObject.GetComponent<Health>().maxHealth += 10;
            gameObject.GetComponent<Health>().health = gameObject.GetComponent<Health>().maxHealth;
            gameObject.GetComponent<Targeting>().waitTime -= 0.3f; 
        }

        //Upgrade sniper
        if (gameObject.name == "Sniper(Clone)")
        {
            gameObject.GetComponent<Health>().health = gameObject.GetComponent<Health>().maxHealth;
            damage += 2;
            gameObject.GetComponent<Targeting>().waitTime -= 0.3f;
        }

        //upgrade slum
        if (gameObject.name == "Slum(Clone)")
        {
            gameObject.GetComponent<Health>().maxHealth += 10;
            gameObject.GetComponent<Health>().health = gameObject.GetComponent<Health>().maxHealth;
            damage += 2;
        }

        //upgrade drone
        if (gameObject.name == "DroneTower(Clone)")
        {
            gameObject.GetComponent<Health>().maxHealth += 10;
            gameObject.GetComponent<Health>().health = gameObject.GetComponent<Health>().maxHealth;
            gameObject.GetComponent<DroneSummoner>().SummonDrone();
        }
        upgrading = false;
    }
}
