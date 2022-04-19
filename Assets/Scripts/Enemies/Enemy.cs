using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
	public float power;
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
	private bool dying;
	private bool dead;
	private float timer;
	private Vector3 direction;

	// Start is called before the first frame update
    void Start()
    {
		moving = true;
		dying = false;
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
			power = Mathf.CeilToInt(power*1.5f);
			health *= 2.5f;
		}
	}

	public void takeDamage(float damage){
		StartCoroutine(blink());
		health -= damage;
		if(health <= 0 && !dying){
			dying = true;
			GameController.instance.credits += bounty;
			dead = true;
		}
	}

	public void kill(){ Destroy(gameObject); }

	public void move(){
		if(moving){ direction = centralTower.position - transform.position; }
		else{ direction = new Vector3(0, 0, 0); }
		direction.Normalize();
		rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
		animator.SetBool("moving", moving);
	}
public Vector3 getDirection(){
  return direction;
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
			resetTimer();
		}
		
		//If attacking slum tower
		else if (collision.gameObject.tag == "Slum" && timer <= 0)
		{
			//If tower has active ability set deal 50% damage
			if(collision.gameObject.GetComponent<Tower>().used)
            {
				collision.gameObject.GetComponent<Health>().takeDamage(power/2);
				Debug.Log("Damage reduced to " + power/2);
			}
			//deal normal damage;
            else
            {
				collision.gameObject.GetComponent<Health>().takeDamage(power);
				Debug.Log("Damage given " + power);
			}
		
			resetTimer();
			//Slum takes damage from attacking enemy
			takeDamage(collision.gameObject.GetComponent<Tower>().damage);
		}
	}


	private void OnCollisionExit2D(Collision2D collision){
		if(collision.gameObject.tag == "Tower"){
			moving = true;
		}
	}
}
