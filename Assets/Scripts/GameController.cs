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
				e.kill();
				break;
			}
		}
		if(currEnemies.Count == 0){
			updateRound();
			if(round == 2){ StartCoroutine(round2()); }
			else if(round == 3){ StartCoroutine(round3()); }
			else if(round == 4){ StartCoroutine(round4()); }
			else if(round == 5){ StartCoroutine(round5()); }
			else if(round == 6){ round6(); }
		}
    }
	
	public void reduceCredits(float cost)
    {
		credits -= cost;
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
	
	private IEnumerator round3(){
		currEnemies.Add(spawnPoints[5].spawnEnemyType(5, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[6].spawnEnemyType(6, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[7].spawnEnemyType(7, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[5].spawnEnemyType(5, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[6].spawnEnemyType(6, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[7].spawnEnemyType(7, 0));
	}
	
	private IEnumerator round4(){
		currEnemies.Add(spawnPoints[29].spawnEnemyType(29, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[21].spawnEnemyType(21, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[25].spawnEnemyType(25, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[29].spawnEnemyType(29, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[21].spawnEnemyType(21, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[25].spawnEnemyType(25, 1));
	}
	
	private IEnumerator round5(){
		currEnemies.Add(spawnPoints[38].spawnEnemyType(38, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[14].spawnEnemyType(14, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[16].spawnEnemyType(16, 1));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[34].spawnEnemyType(34, 0));
		yield return new WaitForSeconds(3);
		currEnemies.Add(spawnPoints[18].spawnEnemyType(18, 1));
	}
	
	private void round6(){
		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 2));
	}
}
