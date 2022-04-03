using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
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
}
