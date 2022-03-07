using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Enemy[] enemies;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public Enemy spawnEnemyType(int type){
		Enemy enemy = Instantiate(enemies[type], transform.position, Quaternion.identity);
        enemy.transform.parent = transform;
		return (Enemy)enemy;
	}
}
