using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        setGround(false);
		setFlying(true);
		speed = 1.5f*speed;
		setPower(1);
		setHealth(20);
		setBounty(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
