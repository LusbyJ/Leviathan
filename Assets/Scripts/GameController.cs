using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : Singleton<GameController>
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
		credits = 500;
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
		if(currEnemies.Count == 0){
			updateRound();
			if(round == 2){ StartCoroutine(round2()); }
		}
    }
	
	private void updateRound(){ round++; }
	
	private void spawnEnemy(int spawn, int type){ 
		currEnemies.Add(spawnPoints[spawn].spawnEnemyType(spawn, type)); 
	}
	
	private IEnumerator round1(){
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[34].spawnEnemyType(34, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[35].spawnEnemyType(35, 0));
	}
	
	private IEnumerator round2(){
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[34].spawnEnemyType(34, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[35].spawnEnemyType(35, 1));
	}
}
