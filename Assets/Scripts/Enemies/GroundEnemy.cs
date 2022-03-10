using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer(Time.deltaTime);
		if(getTimer() <= 0){
			move();
			resetTimer();
		}
    }
}
