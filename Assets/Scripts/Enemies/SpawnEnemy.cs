using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Enemy[] enemies;
	public Transform centralTower;
	
	// Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public Enemy spawnEnemyType(int spawn, int type){
		int order;
		Enemy enemy = Instantiate(enemies[type], transform.position, Quaternion.identity);
		if(spawn < 16 || spawn > 37){ order = -1; }
		else{ order = 2; }
        enemy.transform.parent = transform;
		enemy.centralTower = centralTower;
		enemy.GetComponent<Renderer>().sortingOrder = order;
		return (Enemy)enemy;
	}
}
