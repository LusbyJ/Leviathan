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
        if (upgradeLevel == 2 && gameObject.name != "Central")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = level2Sprite;
        }

        if(upgradeLevel == 3 && gameObject.name != "Central")
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
            //upgrade maxHealth, reset health to max
            Health towerHealth=gameObject.GetComponent<Health>();
            towerHealth.maxHealth *=2;
            towerHealth.health = towerHealth.maxHealth;

            //upgrade fireRate
            gameObject.GetComponent<Targeting>().waitTime *= 0.25f;

            //upgrade Cost
            upgradeCost *= 3;

        }

        //Upgrade sniper
        if (gameObject.name == "Sniper(Clone)")
        {
            //Reset Health to max
            Health towerHealth=gameObject.GetComponent<Health>();
            towerHealth.health = towerHealth.maxHealth;

            //upgrade damage, upgrade fireRate
            damage *=2;
            gameObject.GetComponent<Targeting>().waitTime *= 0.7f;

            //Upgrade cost
            upgradeCost *= 6;
        }

        //upgrade slum
        if (gameObject.name == "Slum(Clone)")
        {
            //Upgrade health, reset health to max
            Health towerHealth=gameObject.GetComponent<Health>();
            towerHealth.maxHealth *=2;
            towerHealth.health = towerHealth.maxHealth;

            //upgrade damage
            damage *= 2;

            //upgrade cost
            upgradeCost *= 4;
        }

        //upgrade drone
        if (gameObject.name == "DroneTower(Clone)")
        {
            //upgrade health, reset health to max
            Health towerHealth=gameObject.GetComponent<Health>();
            towerHealth.maxHealth *=2;
            towerHealth.health = towerHealth.maxHealth;
            //Upgrade/add drone
            DroneSummoner summoner=gameObject.GetComponent<DroneSummoner>();
            for(var i=0;i<upgradeLevel;i++){
                summoner.SummonDrone();
            }

            //upgrade cost
            upgradeCost *= 5;
        }
        upgrading = false;
    }
}
