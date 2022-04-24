using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth=100;   //Tower Max Health
    [HideInInspector]
    public float health;      //Tower health
    private bool hit;       //indicates if hit
    private bool dying;     //indicates if central hub is about to die
    public bool canBeHealed=true; //indicates if the damagable object can be healed by defeating Leviathan or by being next to medical tower.
    public GameObject fire;

    void Start()
    {
        health=maxHealth;
    }

    //Take damage from enemies
    public void takeDamage(float damage)
    {
        //If tower being attacked is not the central hub, blink for each hit
        if (hit == false)
        {
            StartCoroutine("Blink");
            //Take damage
            health -= damage;
        }

        //If central tower being attacked and health is less than 10 blink forever
        if (gameObject.name == "Central" && health < 10 && !dying)
        {
            dying = true;
            InvokeRepeating("BlinkTillDeath", 0, 0.1f);
        }

        //If health is depleted Die
        if (health <= 0)
        {
            Die();
        }
    }

    //Used by the Medical District to heal towers
    public void gainHealth(int healthPickup)
    {
        health += healthPickup;
        health=Mathf.Min(health,maxHealth);
    }

    //Destroy tower
    void Die()
    {
        //If central hub destoryed end game
        if (gameObject.name == "Central")
        {
            SceneManager.LoadScene("GameOver");
        }

        //Remove tower position from tower list and destroy tower
        Vector3Int realPos = gameObject.GetComponent<Tower>().cell;
        GridController.towerList.Remove(realPos);
        GridController.deadTowers.Add(realPos);
        
        //Get Position of tower and adjust offset
        Vector3 firePosition = gameObject.transform.position;
        firePosition.y -= 0.17f;

        //Instantiate fire at tower position
        var rubble = Instantiate(fire, firePosition, Quaternion.identity);
        GridController.fireList.Add(rubble);
        
        //Destroy tower
        Destroy(gameObject);
    }


    //CoRoutine to blink when hit
    private IEnumerator Blink()
    {
        hit = true;
        if (gameObject.GetComponent<Tower>().poisoned)
        {
            for (int i = 0; i < 2; i++)
            {
                GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                GetComponent<Renderer>().material.color = new Color(0.7841f, 1f, 0, 1f);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                GetComponent<Renderer>().material.color = Color.white;
                yield return new WaitForSeconds(0.1f);
            }
        }
        hit = false;
        StopCoroutine("Blink");
    }

    //Repeating function whwn tower is dying
    private void BlinkTillDeath()
    {
        //Blink to red
        if (GetComponent<Renderer>().material.color == Color.white)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        //Blink to white
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

}
