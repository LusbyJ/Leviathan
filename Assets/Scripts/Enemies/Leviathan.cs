using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leviathan : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        setGround(true);
		setFlying(true);
		speed = 0.5f*speed;
		setPower(10);
		setHealth(200);
		setBounty(200);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
