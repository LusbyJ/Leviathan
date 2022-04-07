using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
	public int power;
	public float health;
	public float bounty;
	public Explosion death;
	public Transform centralTower;
	public bool moving;
	public Rigidbody2D rb;
	public float timerInterval;
	public Animator animator;
	
	private	Vector3 movement;
	private bool ground;
	private bool flying;
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
	
	public void setGround(bool grounded){ ground = grounded; }
	
	public void setFlying(bool flies){ flying = flies; }
	
	public bool isGround(){ return ground; }
	
	public bool isFlying(){ return flying; }
	
	public void resetTimer(){ timer = timerInterval; }
	
	public bool isDead(){ return dead; }
	
	public void levelUp(int numLevels){
		for(int i = 0; i < numLevels; i++){
			power *= 2;
			health *= 1.5f;
		}
	}
	
	public void takeDamage(float damage){
		StartCoroutine(blink());
		health -= damage;
		if(health <= 0){ 
			dead = true;
			GameController.instance.credits += bounty;
			explode();
		}
	}
	
	public void kill(){ Destroy(gameObject); }
	
	private void explode(){
		Explosion boom = Instantiate(death, transform.position, Quaternion.identity);
        boom.transform.parent = transform;
	}
	
	public void move(){
		Vector3 direction;
		if(moving){ direction = centralTower.position - transform.position; }
		else{ direction = new Vector3(0, 0, 0); }
		direction.Normalize();
		rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
		animator.SetBool("moving", moving);
	}
	
	private IEnumerator blink(){
		GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material.color = Color.white;
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
			collision.gameObject.GetComponent<Health>().takeDamage(power);
			if(collision.gameObject.name == "Slum")
            {
				takeDamage(collision.gameObject.GetComponent<Tower>().damage);
            }

			resetTimer();
		}
	}
	
	private void OnCollisionExit2D(Collision2D collision){
		if(collision.gameObject.tag == "Tower"){
			moving = true;
		}
	}
}
