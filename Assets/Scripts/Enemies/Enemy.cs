using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
	public Transform centralTower;
	public bool moving;
	public Rigidbody2D rb;
	public float timerInterval;
	
	private	Vector3 movement;
	private bool ground;
	private bool flying;
	private float health;
	private float level;
	private bool dead;
	private float timer;
	
	// Start is called before the first frame update
    void Start()
    {
		moving = true;
		dead = false;
		timer = 1; 
    }
	
	void FixedUpdate(){
		timer -= Time.deltaTime;
		move();
	}
	
	public void setHealth(float life){ health = life; }
	
	public void setGround(bool grounded){ ground = grounded; }
	
	public void setFlying(bool flies){ flying = flies; }
	
	public bool isGround(){ return ground; }
	
	public bool isFlying(){ return flying; }
	
	public void setLevel(float newLevel){ level = newLevel; }
	
	public void resetTimer(){ timer = timerInterval; }
	
	public bool isDead(){ return dead; }
	
	public void takeDamage(float damage){
		health -= damage;
		if(health <= 0){ 
			dead = true;
			GameController.instance.credits += 20;
			}
	}
	
	public void move(){
		Vector3 direction;
		if(moving){ direction = centralTower.position - transform.position; }
		else{ direction = new Vector3(0, 0, 0); }
		direction.Normalize();
		rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
	}
	
	private void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "Enemy"){
			Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
		}
		if(collision.gameObject.tag == "Tower" && moving){
			moving = false;
        }
    }
	
	private void OnCollisionStay2D(Collision2D collision){
		if(collision.gameObject.tag == "Tower" && timer <= 0){
			collision.gameObject.GetComponent<Health>().takeDamage(1);
			resetTimer();
		}
	}
	
	private void OnCollisionExit2D(Collision2D collision){
		if(collision.gameObject.tag == "Tower"){
			moving = true;
		}
	}
}
