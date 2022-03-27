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
		Enemy enemy = Instantiate(enemies[type], transform.position, Quaternion.identity);
        enemy.transform.parent = transform;
		enemy.centralTower = centralTower;
		return (Enemy)enemy;
	}
}
