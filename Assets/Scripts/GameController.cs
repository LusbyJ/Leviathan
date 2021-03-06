using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Tilemaps;

public class GameController : Singleton<GameController>
{
	public SpawnEnemy[] spawnPoints;
	public TMP_Text roundText;
	public TMP_Text creditText;
	public Image levLeft;
	public Image levRight;
	public Image levTop;
	public Image levBottom;
	public Image[] nextRoundUI;
	public Sprite[] enemyUI;
	public Button skipDelayButton;
	public float round;
	public FloatSO ScoreSO;
	public float credits;
	public bool towerPlaced;
	public Tilemap mainMap;
	public Tile emptyTile;
	public bool doTutorial;
	public TutorialTips TutorialUI;

	public AudioSource musicMain;
	public AudioSource musicLeviathan;

	private List<Enemy> currEnemies;
	private List<int[]> thisRound;
	private List<int[]> nextRound;
	private bool delayed;
	private bool delayOver;
	private int maxPoints;
	private int level;
	private int upgrade;
	private int leviathanSpawn;

	public BoolSO Tutorial;
	// Start is called before the first frame update
	void Start()
	{
		//Clear tower lists, then add central tower to list and reset tileMap
		GridController.towerList.Clear();
		GridController.deadTowers.Clear();
		addCentralHub();
		resetTilemap();

		levLeft.enabled = false;
		levRight.enabled = false;
		levTop.enabled = false;
		levBottom.enabled = false;
		skipDelayButton.onClick.AddListener(delayButton);
		skipDelayButton.interactable = false;
		nextRoundUI[0].sprite = enemyUI[0];
		nextRoundUI[1].enabled = false;
		nextRoundUI[2].enabled = false;
		nextRoundUI[3].enabled = false;
		round = 0;
		credits = 1000;
		currEnemies = new List<Enemy>();
		thisRound = new List<int[]>();
		nextRound = new List<int[]>();
		delayed = false;
		delayOver = false;
		maxPoints = 5;
		level = 2;
		upgrade = 0;
		leviathanSpawn = Random.Range(0, 4);
		ScoreSO.Value = 1;
		doTutorial=Tutorial.Value;
		TutorialUI.performRound(0);
		StartCoroutine(tutorialDelay());
	}

	// Update is called once per frame
	void Update()
	{
		roundText.text = "Round " + round;
		creditText.text = "" + credits;
		if (towerPlaced)
		{
			foreach (Enemy e in currEnemies)
			{
				if (e.isDead())
				{
					currEnemies.Remove(e);
					explode(e);
					e.kill();
					break;
				}
			}
			if (currEnemies.Count == 0 && thisRound.Count == 0 && !delayed)
			{
				if(!delayOver && round != 0){ StartCoroutine(roundDelay()); }
				else{
					updateRound();
					refreshTowers();
					leviathanWarning();
					if(round == 10) {
						TutorialUI.performRound(9);
						leviathan();
						delayOver = false;
					}
					if(round == 11) {
						TutorialUI.performRound(10);
					}
					if(round == 21) {
						TutorialUI.performRound(11);
					}
					if (round == 1) { StartCoroutine(round1()); }
					else if (round == 2) { StartCoroutine(round2()); }
					else if (round == 3) { StartCoroutine(round3()); }
					else if (round == 4) { StartCoroutine(round4()); }
					else if (round == 5) { StartCoroutine(round5()); }
					else if (round == 6) { StartCoroutine(round6()); }
					else if (round == 7) { StartCoroutine(round7()); }
					else if (round == 8) { StartCoroutine(round8()); }
					else if (round == 9) { StartCoroutine(round9()); }
					else if (round % 10 == 0)
					{
						warningClear();
						maxPoints += 5;
						StartCoroutine(startRound());
						delayOver = false;
						upgrade++;
						if (level == 2) { level++; }
						musicMain.Stop();
						musicLeviathan.Play();
						GameObject.FindWithTag("VoiceController").GetComponent<VoiceController>().PlayLeviathanClip();
					}
					else
					{
                        if (round % 10 == 1)
                        {
							GameObject.FindWithTag("VoiceController").GetComponent<VoiceController>().PlayRepairClip();
							musicMain.Play();
							musicLeviathan.Stop();
						}
						StartCoroutine(startRound());
						delayOver = false;
					}
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
		foreach (var position in mainMap.cellBounds.allPositionsWithin)
		{
			if (position.x < -8 || position.x > 12 || position.y < -15 || position.y > 14)
			{
				GridController.towerList.Add(position);
			}
		}
	}


	public void reduceCredits(float cost) { credits -= cost; }

	private void explode(Enemy e)
	{
		Explosion boom = Instantiate(e.death, transform.position, Quaternion.identity);
		boom.transform.position = e.transform.position;
	}

	private void updateRound()
	{
		SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.roundStart);
		round++;
		ScoreSO.Value = round;
		resetTowers();
	}

	private void spawnEnemy(int spawn, int type)
	{
		currEnemies.Add(spawnPoints[spawn].spawnEnemyType(spawn, type));
	}

	private void generateRound()
	{
		int[] nextEnemy = new int[2];
		int points = maxPoints;
		int maxEnemy = 2 * level;
		int g = 0;
		int f = 0;
		int gb = 0;
		int fb = 0;
		int gp = 0;
		int fp = 0;
		int i = 0;
		int active = 0;
		while (points > 0)
		{
			nextEnemy[0] = Random.Range(0, 40);
			nextEnemy[1] = Random.Range(0, maxEnemy);
			if (nextEnemy[1] == 0) { g++; }
			else if (nextEnemy[1] == 1) { f++; }
			else if (nextEnemy[1] == 2) { gb++; }
			else if (nextEnemy[1] == 3) { fb++; }
			else if (nextEnemy[1] == 4) { gp++; }
			else if (nextEnemy[1] == 5) { fp++; }
			if (nextEnemy[1] < 2) { points -= 1; }
			else { points -= 2; }
			nextRound.Add(nextEnemy);
			nextEnemy = new int[2];
			if (maxEnemy > 2 && points < 2) { maxEnemy = 2; }
		}
		nextRoundUI[0].enabled = false;
		nextRoundUI[1].enabled = false;
		nextRoundUI[2].enabled = false;
		nextRoundUI[3].enabled = false;
		if (g > 0)
		{
			nextRoundUI[i].sprite = enemyUI[0];
			nextRoundUI[i++].enabled = true;
			active++;
		}
		if (f > 0)
		{
			nextRoundUI[i].sprite = enemyUI[1];
			nextRoundUI[i++].enabled = true;
			active++;
		}
		if (gb > 0)
		{
			nextRoundUI[i].sprite = enemyUI[2];
			nextRoundUI[i++].enabled = true;
		}
		if (fb > 0)
		{
			nextRoundUI[i].sprite = enemyUI[3];
			nextRoundUI[i++].enabled = true;
		}
		if (gp > 0 && i < 4)
		{
			nextRoundUI[i].sprite = enemyUI[4];
			nextRoundUI[i++].enabled = true;
		}
		if (fp > 0 && i < 4)
		{
			nextRoundUI[i].sprite = enemyUI[5];
			nextRoundUI[i++].enabled = true;
		}
	}

	private void leviathan()
	{
		if (leviathanSpawn == 0) { nextRound.Add(new int[] { 5, 6 }); }
		else if (leviathanSpawn == 1) { nextRound.Add(new int[] { 15, 6 }); }
		else if (leviathanSpawn == 2) { nextRound.Add(new int[] { 26, 6 }); }
		else { nextRound.Add(new int[] { 36, 6 }); }
		leviathanSpawn = Random.Range(0, 4);

		nextRoundUI[0].sprite = enemyUI[6];
		nextRoundUI[1].enabled = false;
		nextRoundUI[2].enabled = false;
		nextRoundUI[3].enabled = false;
	}

	private void leviathanWarning()
	{
		if (leviathanSpawn == 0) { levTop.enabled = true; }
		else if (leviathanSpawn == 1) { levRight.enabled = true; }
		else if (leviathanSpawn == 2) { levBottom.enabled = true; }
		else { levLeft.enabled = true; }
	}

	private void refreshTowers()
	{
		//if it's the round after the leviathan
		if (round % 10 == 1)
		{
			resetTilemap();
			GridController.deadTowers.Clear();
			GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
			GameObject[] slums = GameObject.FindGameObjectsWithTag("Slum");
			towers = towers.Concat(slums).ToArray();
			foreach (GameObject tower in towers)
			{
				//Heal
				Health h = tower.GetComponent<Health>();
				if (h.canBeHealed)
				{ //ensure tower is not central tower.
					h.health = h.maxHealth;
				}

				//reset active ability
				Tower a = tower.GetComponent<Tower>();
				a.used = false;
				a.leviathan = false;


			}
			//Destory fires
			foreach (GameObject rubble in GridController.fireList)
			{
				Destroy(rubble);
			}
			GridController.fireList.Clear();
		}
	}

	private void resetTowers()
	{
		GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
		GameObject[] slums = GameObject.FindGameObjectsWithTag("Slum");
		towers = towers.Concat(slums).ToArray();
		foreach (GameObject tower in towers)
		{
			tower.GetComponent<Tower>().resetActive();
		}

	}

	//Reset tilemap tiles
	private void resetTilemap()
	{
		foreach (var position in mainMap.cellBounds.allPositionsWithin)
		{
			if (!mainMap.HasTile(position))
			{
				continue;
			}
			// Tile is not empty; do stuff
			mainMap.SetTile(position, emptyTile);

		}
	}

	private void warningClear()
	{
		levLeft.enabled = false;
		levRight.enabled = false;
		levTop.enabled = false;
		levBottom.enabled = false;
	}

	private IEnumerator startRound()
	{
		int temp;
		if(round == 10 || round == 11 || round == 21){
			if(doTutorial){
				delayed = true;
				yield return new WaitForSeconds(15);
				delayOver = true;
				delayed = false;
			}
		}
		thisRound = new List<int[]>(nextRound);
		nextRound.Clear();
		if (round % 10 == 9) { leviathan(); }
		else { generateRound(); }
		foreach (int[] arr in thisRound)
		{
			currEnemies.Add(spawnPoints[arr[0]].spawnEnemyType(arr[0], arr[1]));
			if (arr[1] < 2)
			{
				temp = arr[0];
				for (int i = 0; i < 4; i++)
				{
					temp++;
					if (temp > 39) { temp = 0; }
					currEnemies.Add(spawnPoints[temp].spawnEnemyType(temp, arr[1]));
				}
			}
			yield return new WaitForSeconds(1);
		}
		foreach (Enemy e in currEnemies) { e.levelUp(upgrade); }
		thisRound.Clear();
	}

	private IEnumerator tutorialDelay(){
		if(doTutorial){
			delayed = true;
			yield return new WaitForSeconds(15);
			delayOver = true;
			delayed = false;
		}
	}

	private IEnumerator roundDelay(){
		if(!doTutorial || (round > 10 && round != 20)){
			delayed = true;
			skipDelayButton.interactable = true;
			if(round % 10 == 0){ yield return new WaitForSeconds(5); }
			else{ yield return new WaitForSeconds(3); }
			skipDelayButton.interactable = false;
		}
		delayOver = true;
		delayed = false;
	}

	void delayButton(){
		StopCoroutine(roundDelay());
		skipDelayButton.interactable = false;
		delayOver = true;
		delayed = false;
	}

	private IEnumerator round1()
	{
		nextRoundUI[0].sprite = enemyUI[0];

		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 0));
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 0));
		currEnemies.Add(spawnPoints[38].spawnEnemyType(38, 0));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[33].spawnEnemyType(33, 0));
		currEnemies.Add(spawnPoints[34].spawnEnemyType(34, 0));
		currEnemies.Add(spawnPoints[35].spawnEnemyType(35, 0));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[35].spawnEnemyType(35, 0));
		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 0));
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 0));
		delayOver = false;
	}

	private IEnumerator round2()
	{
		TutorialUI.performRound(1);
		if(doTutorial){
			delayed = true;
			yield return new WaitForSeconds(15);
			delayOver = true;
			delayed = false;
		}
		nextRoundUI[0].sprite = enemyUI[2];
		thisRound.Add(new int[]{0,0});

		currEnemies.Add(spawnPoints[6].spawnEnemyType(6, 0));
		currEnemies.Add(spawnPoints[7].spawnEnemyType(7, 0));
		currEnemies.Add(spawnPoints[8].spawnEnemyType(8, 0));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[4].spawnEnemyType(4, 0));
		currEnemies.Add(spawnPoints[5].spawnEnemyType(5, 0));
		currEnemies.Add(spawnPoints[6].spawnEnemyType(6, 0));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[25].spawnEnemyType(25, 0));
		currEnemies.Add(spawnPoints[26].spawnEnemyType(26, 0));
		currEnemies.Add(spawnPoints[25].spawnEnemyType(25, 0));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[27].spawnEnemyType(27, 0));
		currEnemies.Add(spawnPoints[28].spawnEnemyType(28, 0));
		currEnemies.Add(spawnPoints[29].spawnEnemyType(29, 0));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[13].spawnEnemyType(13, 0));
		currEnemies.Add(spawnPoints[14].spawnEnemyType(14, 0));
		currEnemies.Add(spawnPoints[15].spawnEnemyType(15, 0));
		thisRound.Clear();
		delayOver = false;
	}

	private IEnumerator round3()
	{
		TutorialUI.performRound(2);
		if(doTutorial){
			delayed = true;
			yield return new WaitForSeconds(15);
			delayOver = true;
			delayed = false;
		}
		nextRoundUI[0].sprite = enemyUI[1];
		thisRound.Add(new int[]{0,0});

		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 2));
		yield return new WaitForSeconds(2);
		thisRound.Clear();
		delayOver = false;
	}

	private IEnumerator round4()
	{
		TutorialUI.performRound(3);
		if(doTutorial){
			delayed = true;
			yield return new WaitForSeconds(15);
			delayOver = true;
			delayed = false;
		}
		nextRoundUI[0].sprite = enemyUI[1];
		thisRound.Add(new int[]{0,0});

		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 1));
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 1));
		currEnemies.Add(spawnPoints[38].spawnEnemyType(38, 1));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[33].spawnEnemyType(33, 1));
		currEnemies.Add(spawnPoints[34].spawnEnemyType(34, 1));
		currEnemies.Add(spawnPoints[35].spawnEnemyType(35, 1));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[35].spawnEnemyType(35, 1));
		currEnemies.Add(spawnPoints[36].spawnEnemyType(36, 1));
		currEnemies.Add(spawnPoints[37].spawnEnemyType(37, 1));
		thisRound.Clear();
		delayOver = false;
	}

	private IEnumerator round5()
	{
		TutorialUI.performRound(4);
		if(doTutorial){
			delayed = true;
			yield return new WaitForSeconds(15);
			delayOver = true;
			delayed = false;
		}
		nextRoundUI[0].sprite = enemyUI[3];
		thisRound.Add(new int[]{0,0});

		currEnemies.Add(spawnPoints[6].spawnEnemyType(6, 1));
		currEnemies.Add(spawnPoints[7].spawnEnemyType(7, 1));
		currEnemies.Add(spawnPoints[8].spawnEnemyType(8, 1));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[4].spawnEnemyType(4, 1));
		currEnemies.Add(spawnPoints[5].spawnEnemyType(5, 1));
		currEnemies.Add(spawnPoints[6].spawnEnemyType(6, 1));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[25].spawnEnemyType(25, 1));
		currEnemies.Add(spawnPoints[26].spawnEnemyType(26, 1));
		currEnemies.Add(spawnPoints[27].spawnEnemyType(27, 1));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[27].spawnEnemyType(27, 1));
		currEnemies.Add(spawnPoints[28].spawnEnemyType(28, 1));
		currEnemies.Add(spawnPoints[29].spawnEnemyType(29, 1));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[13].spawnEnemyType(13, 1));
		currEnemies.Add(spawnPoints[14].spawnEnemyType(14, 1));
		currEnemies.Add(spawnPoints[15].spawnEnemyType(15, 1));
		thisRound.Clear();
		delayOver = false;
	}

	private IEnumerator round6()
	{
		TutorialUI.performRound(5);
		if(doTutorial){
			delayed = true;
			yield return new WaitForSeconds(15);
			delayOver = true;
			delayed = false;
		}
		nextRoundUI[0].sprite = enemyUI[1];
		nextRoundUI[1].sprite = enemyUI[2];
		nextRoundUI[1].enabled = true;
		thisRound.Add(new int[]{0,0});

		currEnemies.Add(spawnPoints[15].spawnEnemyType(15, 3));
		yield return new WaitForSeconds(2);
		thisRound.Clear();
		delayOver = false;
	}

	private IEnumerator round7()
	{
		TutorialUI.performRound(6);
		if(doTutorial){
			delayed = true;
			yield return new WaitForSeconds(15);
			delayOver = true;
			delayed = false;
		}
		nextRoundUI[0].sprite = enemyUI[0];
		nextRoundUI[1].sprite = enemyUI[3];
		thisRound.Add(new int[]{0,0});

		currEnemies.Add(spawnPoints[26].spawnEnemyType(26, 2));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[23].spawnEnemyType(23, 1));
		currEnemies.Add(spawnPoints[24].spawnEnemyType(24, 1));
		currEnemies.Add(spawnPoints[25].spawnEnemyType(25, 1));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[27].spawnEnemyType(27, 1));
		currEnemies.Add(spawnPoints[28].spawnEnemyType(28, 1));
		currEnemies.Add(spawnPoints[29].spawnEnemyType(29, 1));
		thisRound.Clear();
		delayOver = false;
	}

	private IEnumerator round8()
	{
		TutorialUI.performRound(7);
		if(doTutorial){
			delayed = true;
			yield return new WaitForSeconds(15);
			delayOver = true;
			delayed = false;
		}
		nextRoundUI[0].sprite = enemyUI[2];
		nextRoundUI[1].sprite = enemyUI[3];
		thisRound.Add(new int[]{0,0});

		currEnemies.Add(spawnPoints[6].spawnEnemyType(6, 3));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[3].spawnEnemyType(3, 0));
		currEnemies.Add(spawnPoints[4].spawnEnemyType(4, 0));
		currEnemies.Add(spawnPoints[5].spawnEnemyType(5, 0));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[7].spawnEnemyType(7, 0));
		currEnemies.Add(spawnPoints[8].spawnEnemyType(8, 0));
		currEnemies.Add(spawnPoints[9].spawnEnemyType(9, 0));
		thisRound.Clear();
		delayOver = false;
	}

	private IEnumerator round9()
	{
		TutorialUI.performRound(8);
		if(doTutorial){
			delayed = true;
			yield return new WaitForSeconds(15);
			delayOver = true;
			delayed = false;
		}
		nextRoundUI[0].sprite = enemyUI[6];
		nextRoundUI[1].enabled = false;
		thisRound.Add(new int[]{0,0});

		currEnemies.Add(spawnPoints[39].spawnEnemyType(39, 2));
		yield return new WaitForSeconds(2);
		currEnemies.Add(spawnPoints[19].spawnEnemyType(19, 3));
		thisRound.Clear();
		delayOver = false;
	}
}
