using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
	
	private float health;
	private float level;
	private bool dead;
	
	// Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void setLevel(float newLevel){ level = newLevel; }
	
	public void takeDamage(float damage){
		health -= damage;
		if(health <= 0){ dead = true; }
	}
	
	public bool isDead(){ return dead; }
}
