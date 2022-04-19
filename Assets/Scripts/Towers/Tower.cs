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
    public bool abilityReady=false;
    public float upgradeCost;
    public GameObject projectile;
    public bool OverrideTargetting=false; //Used for medical tower active
    private bool upgrading = false;
    private bool activeAbility = false;
    public bool used = false;

    public Sprite level2Sprite;
    public Sprite level3Sprite;

    public Animator animator;

    //Active abilities 
    private float basicRate; //Gunner
    private float activeRate; //Gunner

    private float basicDamage; //Sniper

    void Start()
    {
        basicRate = attackTime;
        activeRate = basicRate * 2;

        basicDamage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradeLevel == 2 && gameObject.name != "Central")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = level2Sprite;
            animator.SetBool("isLevel2", true);
        }

        if(upgradeLevel == 3 && gameObject.name != "Central")
        {
            if(used)
            {
                animator.SetBool("isActive", false);
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = level3Sprite;
            if (!used)
            {
                animator.SetBool("isLevel3", true);
                activeAbility = true;
                animator.SetBool("isActive", true);
            }
        }

        //Check for used bool to be changed to false when game Controller hits round%10 = 1
        if(used == false)
        {
            resetActive();
        }
    }

    //Detects button clicks when mouse is over tower
    void OnMouseOver()
    {
        //If right click use active ability
        if(Input.GetMouseButtonDown(1) && activeAbility && !used)
        {
            applyActive();
            animator.SetBool("isActive", false);
            used = true;
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
            upgradeCost *= 3;
        }

        //upgrade slum
        if (gameObject.name == "Slum(Clone)")
        {
            //Upgrade health, reset health to max
            Health towerHealth=gameObject.GetComponent<Health>();
            towerHealth.maxHealth *=2;
            towerHealth.health = towerHealth.maxHealth;

            //upgrade damage
            damage *= 3;

            //upgrade cost
            upgradeCost *= 3;
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
            upgradeCost *= 3;
        }
        upgrading = false;
    }

    //Resets the towers active abilities
    public void resetActive()
    {
        attackTime = basicRate;
        damage = basicDamage;
    }

    //Sets the active ability
    public void applyActive()
    {
        if (gameObject.name == "Gunner(Clone)")
        {
            Debug.Log("active ability being used");
            attackTime = activeRate;
        }
    }
}
