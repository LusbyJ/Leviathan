using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBruiser : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        setGround(false);
		setFlying(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public override void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
		}
		if (collision.gameObject.tag == "Tower" && moving)
		{
			moving = false;
			collision.gameObject.GetComponent<Health>().takeDamage(power);
			takeDamage(health);
		}
		if(collision.gameObject.tag == "Slime")
        {
			Destroy(collision.gameObject);
			poison();
        }
	}
}
