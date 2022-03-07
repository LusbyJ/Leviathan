using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public SpawnEnemy[] spawnPoints;
	public TMP_Text roundText;
	public TMP_Text creditText;
	public float round;
	public float credits;
	
	private List<Enemy> currEnemies;
	
	// Start is called before the first frame update
    void Start()
    {
        round = 1;
		credits = 5;
		currEnemies = new List<Enemy>();
		currEnemies.Add(spawnPoints[0].spawnEnemyType(0));
    }

    // Update is called once per frame
    void Update()
    {
        roundText.text = "Round " + round;
		creditText.text = "Credits: " + credits;
		foreach(Enemy e in currEnemies){
			if(e.isDead()){
				currEnemies.Remove(e);
				Destroy(e);
			}
		}
    }
	
	private void updateRound(){ round++; }
}
