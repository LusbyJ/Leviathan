using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
	public float tileDist;
	public Transform centralTower;
	public bool moving;
	
	public Rigidbody2D rb;
	private	Vector3 movement;
	private float health;
	private float level;
	private bool dead;
	private float timer;
	
	// Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
		moving = true;
		dead = false;
		timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }
	
	void FixedUpdate(){
        //Debug.Log(movement);
		moveEnemy(movement);
	}
	
	public void setLevel(float newLevel){ level = newLevel; }
	
	public float getTimer(){ return timer; }
	
	public void updateTimer(float delta){ timer -= delta; }
	
	public void resetTimer(){ timer = speed; }
	
	public void takeDamage(float damage){
		health -= damage;
		if(health <= 0){ dead = true; }
	}
	
	private void moveEnemy(Vector3 direction){
		rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
	}
	
	public bool isDead(){ return dead; }
	
	public void move(){
		Vector3 direction;
		if(moving){ direction = centralTower.position - transform.position; }
		else{ direction = new Vector3(0, 0, 0); }
		direction.Normalize();
		movement = direction;
	}
	
	public abstract void attack();
}
