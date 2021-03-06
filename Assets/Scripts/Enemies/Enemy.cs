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
	public bool attacking;
	public bool poisoned;
	public Rigidbody2D rb;
	public float timer;
	public float timerInterval;
	public float poisonDuration;
	public Animator animator;

	private Vector3 movement;
	private bool ground;
	private bool flying;
	private bool stopMoving;
	private bool dying;
	private bool dead;
	private float poisonTimer;
	private Vector3 direction;
	private Vector3 realDirection;

	// Start is called before the first frame update
	void Start()
	{
		moving = true;
		stopMoving = false;
		attacking = false;
		poisoned = false;
		dying = false;
		dead = false;
		timer = timerInterval;
		poisonTimer = 0;
		realDirection = new Vector3(0, 0, 0);
	}

	void FixedUpdate()
	{
		timer -= Time.deltaTime;
		if (poisoned)
		{
			poisonTimer -= Time.deltaTime;
			if(poisonTimer <= 0){
				poisoned = false;
				GetComponent<Renderer>().material.color = Color.white;
			}
		}
		if(stopMoving){ 
			moving = false; 
			stopMoving = false;
		}
		move();
		animator.SetBool("attack", attacking);
	}

	public void setGround(bool grounded) { ground = grounded; }

	public void setFlying(bool flies) { flying = flies; }

	public bool isGround() { return ground; }

	public bool isFlying() { return flying; }

	public void resetTimer() { timer = timerInterval; }

	public void poison()
	{
		poisoned = true;
		poisonTimer = poisonDuration;
		GetComponent<Renderer>().material.color = new Color(0.7841f, 1f, 0, 1f);
	}

	public bool isDead() { return dead; }

	public void levelUp(int numLevels)
	{
		for (int i = 0; i < numLevels; i++)
		{
			power = Mathf.CeilToInt(power * 1.5f);
			health *= 2.5f;
		}
	}

	public void takeDamage(float damage)
	{
		StartCoroutine(blink());
		if(poisoned){ health -= (2*damage); }
		else{ health -= damage; }
		if (health <= 0 && !dying)
		{
			dying = true;
			GameController.instance.credits += bounty;
			dead = true;
		}
	}

	public void kill() { 
		if (gameObject.name == "GroundBruiser(Clone)" || gameObject.name == "FlyingBruiser(Clone)")
		{
			SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.bruiserDie);
		}
		if(gameObject.name == "Leviathan(Clone)")
        {
			SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.leviathanDie);

		}
        else
        {
			SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.smallFoeDie);
		}
		Destroy(gameObject);
 }

	public void move()
	{
		float move;
		if (poisoned) { move = speed / 4; }
		else { move = speed; }
		if (moving) {
			//Debug.Log("Attacking" + attacking);
			direction = centralTower.position - transform.position;
			realDirection = direction;
			realDirection.Normalize();
			direction.Normalize();
			rb.MovePosition(transform.position + (direction * move * Time.deltaTime));
			if(timer <= -1){ attacking = false; }
		}
		else { 
			direction = new Vector3(0, 0, 0); 
			if(timer <= -1){ moving = true; }
		}
		
	}

	public Vector3 getDirection()
	{
		return realDirection;
	}

	private IEnumerator blink()
	{
		GetComponent<Renderer>().material.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		if(poisoned){ GetComponent<Renderer>().material.color = new Color(0.7841f, 1f, 0, 1f); }
		else{ GetComponent<Renderer>().material.color = Color.white; }
	}

	public virtual void OnCollisionEnter2D(Collision2D collision)
	{
		//Debug.Log("Entering");
		if (collision.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
		}
		if (collision.gameObject.tag == "Tower" && moving)
		{
			stopMoving = true;
			attacking = true;
		}
		if (collision.gameObject.tag == "Slum" && moving)
		{
			stopMoving = true;
			attacking = true;
		}
		if(collision.gameObject.tag == "Slime")
        {
			Destroy(collision.gameObject);
			poison();
        }
	}

	public virtual void OnCollisionStay2D(Collision2D collision)
	{
		//Debug.Log("Colliding");
		if (collision.gameObject.tag == "Tower" && timer <= 0)
		{
			collision.gameObject.GetComponent<Health>().takeDamage(power);
			resetTimer();
			//Debug.Log("Attacking");
		}

		//If attacking slum tower
		else if (collision.gameObject.tag == "Slum" && timer <= 0)
		{
			//If tower has active ability set deal 50% damage
			if (collision.gameObject.GetComponent<Tower>().used)
			{
				collision.gameObject.GetComponent<Health>().takeDamage(power / 2);
			}
			//deal normal damage;
			else
			{
				collision.gameObject.GetComponent<Health>().takeDamage(power);
			}

			resetTimer();
			//Slum takes damage from attacking enemy
			takeDamage(collision.gameObject.GetComponent<Tower>().damage);
		}
	}


	private void OnCollisionExit2D(Collision2D collision)
	{
		//Debug.Log("Exiting");
		if (collision.gameObject.tag == "Tower")
		{
			moving = true;
			attacking = false;
		}
	}
}
