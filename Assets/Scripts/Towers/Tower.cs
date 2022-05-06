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
    public bool abilityReady = false;
    public float upgradeCost;
    public GameObject projectile;
    public bool OverrideTargetting = false; //Used for medical tower active
    public bool used = false;
    public bool leviathan = false;
    public bool poisoned;
    public bool current;
    public Sprite level2Sprite;
    public Sprite level3Sprite;
    public Animator animator;

    private bool upgrading = false;
    private bool activeAbility = false;

    //Active abilities 
    private float basicRate; //Gunner
    private float activeRate; //Gunner

    private float basicDamage; //Sniper

    void Start()
    {
        basicRate = attackTime;
        activeRate = basicRate * 2;
        poisoned = false;
        current = false;
        basicDamage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        //If poisoned, turn green and take damage every second
        if (poisoned && !current)
        {
            GetComponent<Renderer>().material.color = new Color(0.7841f, 1f, 0, 1f);
            current = true;
            StartCoroutine("applyPoison");
        }
        if (upgradeLevel == 2 && gameObject.name != "Central")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = level2Sprite;
            animator.SetBool("isLevel2", true);
        }

        if (upgradeLevel == 3 && gameObject.name != "Central")
        {

            if (used)
            {
                animator.SetBool("isActive", false);
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = level3Sprite;
            if (!used && !leviathan)
            {
                animator.SetBool("isLevel3", true);
                activeAbility = true;
                animator.SetBool("isActive", true);
            }
            else
            {
                animator.SetBool("isActive", false);
            }
        }
    }

    //Take damage every second for 5 seconds
    private IEnumerator applyPoison()
    {
        for (int i = 0; i < 5; i++)
        {
            gameObject.GetComponent<Health>().takeDamage(2);
            yield return new WaitForSeconds(1f);
        }
        poisoned = false;
        current = false;
        GetComponent<Renderer>().material.color = Color.white;
        StopCoroutine("applyPoison");
    }

    //Detects button clicks when mouse is over tower
    void OnMouseOver()
    {
        //If left click check if credits are sufficient and upgrade
        if (Input.GetMouseButtonDown(0))
        {
            if (GameController.instance.credits >= upgradeCost && upgradeLevel < 3)
            {
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.upgradeTower);
                GameController.instance.credits -= upgradeCost;
                upgradeLevel++;

                upgrading = true;
                applyUpgrades();
            }

            if (activeAbility && !used && !leviathan)
            {
                applyActive();
                animator.SetBool("isActive", false);
                used = true;
                leviathan = true;
            }
        }
    }

    //Apply tower specific upgrades then reset upgrading bool
    public void applyUpgrades()
    {
        //Upgrade Gunner
        if (gameObject.name == "Gunner(Clone)")
        {
            //upgrade maxHealth, reset health to max
            Health towerHealth = gameObject.GetComponent<Health>();
            towerHealth.maxHealth *= 2;
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
            Health towerHealth = gameObject.GetComponent<Health>();
            towerHealth.health = towerHealth.maxHealth;

            //upgrade damage, upgrade fireRate
            damage *= 2;
            gameObject.GetComponent<Targeting>().waitTime *= 0.7f;

            //Upgrade cost
            upgradeCost *= 3;
        }

        //upgrade slum
        if (gameObject.name == "Slum(Clone)")
        {
            //Upgrade health, reset health to max
            Health towerHealth = gameObject.GetComponent<Health>();
            towerHealth.maxHealth *= 2;
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
            Health towerHealth = gameObject.GetComponent<Health>();
            towerHealth.maxHealth *= 2;
            towerHealth.health = towerHealth.maxHealth;

            //Upgrade/add drone
            DroneSummoner summoner = gameObject.GetComponent<DroneSummoner>();
            for (var i = 0; i < upgradeLevel; i++)
            {
                summoner.SummonDrone();
            }

            //upgrade cost
            upgradeCost *= 3;
        }

        //upgrade Medical
        if (gameObject.name == "Medical(Clone)")
        {
            //upgrade health, reset health to max
            Health towerHealth = gameObject.GetComponent<Health>();
            towerHealth.maxHealth *= 2;
            towerHealth.health = towerHealth.maxHealth;

            //upgrade heal per second
            gameObject.GetComponent<Medical>().healingTime -= 5;
        }

        //Upgrade chemical
        if (gameObject.name == "Chemical(Clone)")
        {
            //reset health to max
            Health towerHealth = gameObject.GetComponent<Health>();
            towerHealth.health = towerHealth.maxHealth;

            //Increase fireRate
            gameObject.GetComponent<Targeting>().waitTime -= 0.8f;

            //Increase range
            gameObject.GetComponent<Targeting>().TargetDist += 0.5f;
        }

        //Upgrade Missile
        if (gameObject.name == "Missile(Clone)")
        {
            //reset health to max
            Health towerHealth = gameObject.GetComponent<Health>();
            towerHealth.health = towerHealth.maxHealth;

            //upgrade fireRate
            gameObject.GetComponent<Targeting>().waitTime -= 2f;

            //Increase range
            gameObject.GetComponent<Targeting>().TargetDist += 0.5f;

            //upgrade Cost
            upgradeCost *= 3;
        }

        //Upgrade Nuclear
        if (gameObject.name == "Nuclear(Clone)")
        {
            //upgrade health, reset health to max
            Health towerHealth = gameObject.GetComponent<Health>();
            towerHealth.maxHealth *= 2;
            towerHealth.health = towerHealth.maxHealth;

            //Increase damage per second
            gameObject.GetComponent<Nuclear>().radiationTime /= 2;

            //upgrade Cost
            upgradeCost *= 3;
        }
        upgrading = false;
    }

    //Resets the towers active abilities
    public void resetActive()
    {
        //Slum resets in Enemy Script

        //reset attack rate (Gunner)
        attackTime = basicRate;

        //resets damage(used for sniper)
        damage = basicDamage;

        //Change adjacent tower targetting
        if (gameObject.name == "Medical(Clone)")
        {
            gameObject.GetComponent<Medical>().changeActive(false);
        }

        //remove extra drone summoned 
        if (gameObject.name == "DroneTower(Clone)")
        {
            gameObject.GetComponent<DroneSummoner>().resetDrones();
        }

        used = false;
    }

    //Sets the active ability
    //Slum active ability managed in Enemy Script
    //Sniper active ability managed in Targeting script
    public void applyActive()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.activateAbility);
        //Update values for gunner
        if (gameObject.name == "Gunner(Clone)")
        {
            attackTime = activeRate;
        }

        //summon new drones
        if (gameObject.name == "DroneTower(Clone)")
        {
            DroneSummoner summoner = gameObject.GetComponent<DroneSummoner>();
            summoner.extraDrone();
        }

        //Set adjacecent towers to medical
        if (gameObject.name == "Medical(Clone)")
        {
            gameObject.GetComponent<Medical>().changeActive(true);
        }

        //Slimes all enemies in range
        if (gameObject.name == "Chemical(Clone)")
        {
            gameObject.GetComponent<Targeting>().shootEverybody();
        }

        //Shoots rocket at all enemies in range
        if (gameObject.name == "Missile(Clone)")
        {
            gameObject.GetComponent<Targeting>().shootEverybody();
        }

        //Overload plant and deal high damage to all enemies in range
        if (gameObject.name == "Nuclear(Clone)")
        {
            gameObject.GetComponent<Nuclear>().useActive();
        }
    }
}
