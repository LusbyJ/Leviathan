using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    private bool hit;

    //Take damage from enemies
    public void takeDamage(int damage)
    {
        //StartCoroutine("Blink");
        health -= damage;
        if (health <= 0)
            Die();
    }

    //Used by the Medical District to heal towers
    public void gainHealth(int healthPickup)
    {
        health += healthPickup;
    }

    //Destroy tower if health depleted
    void Die()
    {
        Destroy(gameObject);
    }

    /*
    Tower flashes when hit, iframes initiated 
    ----This can be implemented if needed---
    
    private IEnumerator Blink()
    {
        hit = true;
        for (int i = 0; i < 5; i++)
        {
            GetComponent<Renderer>().material.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        hit = false;
        StopCoroutine("Blink");
    }
    */

}
