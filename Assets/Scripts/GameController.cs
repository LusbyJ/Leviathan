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
		StartCoroutine(round1());
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
	
	private void spawnEnemy(int spawn, int type){ 
		currEnemies.Add(spawnPoints[spawn].spawnEnemyType(type)); 
	}
	
	private IEnumerator round1(){
		currEnemies.Add(spawnPoints[37].spawnEnemyType(0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[34].spawnEnemyType(0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[36].spawnEnemyType(0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[37].spawnEnemyType(0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[35].spawnEnemyType(0));
	}
}
