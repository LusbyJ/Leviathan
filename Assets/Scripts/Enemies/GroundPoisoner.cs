using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoisoner : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        setGround(true);
		setFlying(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public override void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Tower" && timer <= 0)
		{
			collision.gameObject.GetComponent<Tower>().poisoned = true;
		} else if (collision.gameObject.tag == "Slum" && timer <= 0)
		{
			collision.gameObject.GetComponent<Tower>().poisoned = true;
		}
		base.OnCollisionStay2D(collision);
	}
}
