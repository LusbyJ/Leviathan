using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
	// Start is called before the first frame update
    void Start()
    {
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
		move();
		if(!moving){ attack(); }
    }
	
	public override void attack(){}
	
	private void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "Enemy"){
			Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
		}
		if(collision.gameObject.tag == "Tower" && moving){
			moving = false;
        }
    }
	
	private void OnCollisionExit2D(Collision2D collision){
		if(collision.gameObject.tag == "Tower"){
			moving = true;
		}
	}
}
