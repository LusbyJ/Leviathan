using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
	public float tileDist;
	
	private float health;
	private float level;
	private bool dead;
	private float timer;
	
	// Start is called before the first frame update
    void Start()
    {
        dead = false;
		timer = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void setLevel(float newLevel){ level = newLevel; }
	
	public float getTimer(){ return timer; }
	
	public void updateTimer(float delta){ timer -= delta; }
	
	public void resetTimer(){ timer = speed; }
	
	public void takeDamage(float damage){
		health -= damage;
		if(health <= 0){ dead = true; }
	}
	
	public bool isDead(){ return dead; }
	
	public void move(){
		Vector3 direction;
		if(transform.position.y < 0){
			if(transform.position.x == 0){
				direction = new Vector3(0, 1, 0);
			} else if(transform.position.x < 0){
				direction = new Vector3(0.5f, 0.5f, 0);
			} else {
				direction = new Vector3(-0.5f, 0.5f, 0);
			}
		} else{
			if(transform.position.x == 0){
				direction = new Vector3(0, -1, 0);
			} else if(transform.position.x < 0){
				direction = new Vector3(0.5f, -0.5f, 0);
			} else {
				direction = new Vector3(-0.5f, -0.5f, 0);
			}
		}
		transform.position += direction;
	}
}
