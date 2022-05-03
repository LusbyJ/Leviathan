using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuclear : MonoBehaviour
{
    public GameObject Owner;     //This tower
    public float radiationTime;  //Time between damage delivery
    public float damage;         //Damage amount    
    public float TargetDist;     //Range of damage distibution

    
    // Immediately start dealing damage to enemies in range 
    void Start()
    {
        InvokeRepeating("radiationPoisoning", 0, radiationTime);
    }

    //Deal damage continuosly to all enemies in range
    private void radiationPoisoning()
    {
        Enemy[] PotentialFoes = FindObjectsOfType(typeof(Enemy)) as Enemy[];
        foreach (Enemy foeScr in PotentialFoes)
        {
            GameObject foe = foeScr.gameObject;
            Vector2 FoeVector = new Vector2(Owner.transform.position.x, Owner.transform.position.y) - new Vector2(foe.transform.position.x, foe.transform.position.y);
            float FoeDist = FoeVector.magnitude;

            if (FoeDist < TargetDist && foeScr.tag == "Enemy")
            {
                foeScr.GetComponent<Enemy>().takeDamage(damage);
            }
        }
    }

    //Deals high damage to all enemies in range
    public void useActive()
    {
        Enemy[] PotentialFoes = FindObjectsOfType(typeof(Enemy)) as Enemy[];
        foreach (Enemy foeScr in PotentialFoes)
        {
            GameObject foe = foeScr.gameObject;
            Vector2 FoeVector = new Vector2(Owner.transform.position.x, Owner.transform.position.y) - new Vector2(foe.transform.position.x, foe.transform.position.y);
            float FoeDist = FoeVector.magnitude;

            if (FoeDist < TargetDist && foeScr.tag == "Enemy")
            {
                foeScr.GetComponent<Enemy>().takeDamage(damage*20);
            }
        }
    }
}
