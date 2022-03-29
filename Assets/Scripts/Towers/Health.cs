using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;      //Tower health  
    private bool hit;       //indicates if hit
    private bool dying;     //indicates if central hub is about to die

    //Take damage from enemies
    public void takeDamage(int damage)
    {
        //If tower being attacked is not the central hub, blink for each hit
        if (gameObject.name != "Central")
        {
            StartCoroutine("Blink");
        }

        //If central tower being attacked and health is less than 10 blink forever
        if (gameObject.name == "Central" && health < 10 && !dying)
        {
            dying = true;
            InvokeRepeating("BlinkTillDeath", 0, 0.1f);

        }

        //Take damage
        health -= damage;

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
    }

    //Destroy tower
    void Die()
    {
        if (gameObject.name == "Central")
        {
            Debug.Log("Made it to next scene");
            //CancelInvoke("BlinkTillDeath");
            SceneManager.LoadScene("GameOver");
        }
        Destroy(gameObject);
    }


    //CoRoutine to blink when hit
    private IEnumerator Blink()
    {
        hit = true;
        for (int i = 0; i < 2; i++)
        {
            GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
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
