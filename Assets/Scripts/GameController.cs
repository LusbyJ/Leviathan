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
	public Image levLeft;
	public Image levRight;
	public Image levTop;
	public Image levBottom;
	public float round;
	public float credits;
	public bool towerPlaced;

	private List<Enemy> currEnemies;
	private List<int[]> nextRound;
	private int maxPoints;
	private int level;
	private int upgrade;
	private int leviathanSpawn;
	//add transform to spawn explosion? or do it in Enemy class?

	// Start is called before the first frame update
    void Start()
    {
        levLeft.enabled = false;
		levRight.enabled = false;
		levTop.enabled = false;
		levBottom.enabled = false;
		round = 0;
		credits = 500;
		currEnemies = new List<Enemy>();
		nextRound = new List<int[]>();
		maxPoints = 5;
		level = 1;
		upgrade = 0;
		leviathanSpawn = Random.Range(0, 4);
		addCentralHub();
	}

    // Update is called once per frame
    void Update()
    {
        if(towerPlaced){
			roundText.text = "Round " + round;
			creditText.text = "Credits: " + credits;

			foreach(Enemy e in currEnemies){
				if(e.isDead()){
					currEnemies.Remove(e);
					e.kill();
					break;
				}
			}
			if(currEnemies.Count == 0 && nextRound.Count == 0){
				updateRound();
        refreshTowers();
				leviathanWarning();
				if(round == 1){ StartCoroutine(round1()); }
				else if(round == 2){ StartCoroutine(round2()); }
				else if(round%10 == 0){
					leviathan();
					warningClear();
					StartCoroutine(startRound());
					upgrade++;
					maxPoints += 5;
					if(level == 1){ level++; }
				}
				else{
					generateRound();
					StartCoroutine(startRound());
				}
			}
		}
    }

	//Adds cell positions of central hub to the list of tower positions
	public void addCentralHub()
	{
		GridController.towerList.Add(new Vector3Int(1, -2, 0));
		GridController.towerList.Add(new Vector3Int(0, -1, 0));
		GridController.towerList.Add(new Vector3Int(1, 0, 0));
		GridController.towerList.Add(new Vector3Int(1, -1, 0));
		GridController.towerList.Add(new Vector3Int(2, 0, 0));
		GridController.towerList.Add(new Vector3Int(2, -1, 0));
		GridController.towerList.Add(new Vector3Int(2, -2, 0));
	}


	public void reduceCredits(float cost){ credits -= cost; }

	private void updateRound(){
		round++;
	}

	private void spawnEnemy(int spawn, int type){
		currEnemies.Add(spawnPoints[spawn].spawnEnemyType(spawn, type));
	}

	private void generateRound(){
		int[] nextEnemy = new int[2];
		int points = maxPoints;
		int maxEnemy = 2*level;
		while(points > 0){
			nextEnemy[0] = Random.Range(0, 40);
			nextEnemy[1] = Random.Range(0, maxEnemy);
			if(nextEnemy[1] < 2){ points -= 1; }
			else{ points -= 2; }
			nextRound.Add(nextEnemy);
			nextEnemy = new int[2];
			if(maxEnemy > 2 && points < 2){ maxEnemy = 2; }
		}
	}

	private void leviathan(){
		if(leviathanSpawn == 0){ nextRound.Add(new int[]{5, 4}); }
		else if(leviathanSpawn == 1){ nextRound.Add(new int[]{15, 4}); }
		else if(leviathanSpawn == 2){ nextRound.Add(new int[]{26, 4}); }
		else{ nextRound.Add(new int[]{36, 4}); }
		leviathanSpawn = Random.Range(0, 4);
	}

	private void leviathanWarning(){
		if(leviathanSpawn == 0){ levTop.enabled = true; }
		else if(leviathanSpawn == 1){ levRight.enabled = true; }
		else if(leviathanSpawn == 2){ levBottom.enabled = true; }
		else{ levLeft.enabled = true; }
	}

  private void refreshTowers(){
    //if it's the round after the leviathan
    if(round%10==1){
      GameObject[] towers=GameObject.FindGameObjectsWithTag("Tower");
      foreach(GameObject tower in towers){
        //Heal
        Health h=tower.GetComponent<Health>();
        if(h.canBeHealed){ //ensure tower is not central tower.
          h.health=h.maxHealth;
        }
        //Refresh Abilities
        //TODO\\
      }
    }
  }

	private void warningClear(){
		levLeft.enabled = false;
		levRight.enabled = false;
		levTop.enabled = false;
		levBottom.enabled = false;
	}

	private IEnumerator startRound(){
		foreach(int[] arr in nextRound){
			currEnemies.Add(spawnPoints[arr[0]].spawnEnemyType(arr[0], arr[1]));
			yield return new WaitForSeconds(1);
		}
		foreach(Enemy e in currEnemies){ e.levelUp(upgrade); }
		nextRound.Clear();
	}

	private IEnumerator round1(){
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 0));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[34].spawnEnemyType(34, 0));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 0));
	}

	private IEnumerator round2(){
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 1));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[34].spawnEnemyType(34, 1));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 1));
	}
}
