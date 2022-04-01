using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public GameObject Owner;
    private GameObject Target;
    private Vector2 Speed;
    public float TargetDist;
    public float Accel = 0.01f;
    public float MaxSpeed = 2f;
    public displayObject Display;
    public float waitTime = 2f;
    private float AttackTimer;
    public GameObject DroneProjectile;
    private Tower OwnerTower;
    // Start is called before the first frame update
    void Start()
    {
        Target = Owner;
        Display = gameObject.GetComponent<displayObject>();
        AttackTimer = 0;
        OwnerTower = Owner.GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Owner)
        {
            Destroy(this);
        }
        else
        {
            //Reset the target if it doesn't exist or if it's out of range.
            if (!Target)
            {
                Target = Owner;
            }
         
            //If there is no target, find a new one
            if (Target == Owner)
            {
                Enemy[] PotentialFoes = FindObjectsOfType(typeof(Enemy)) as Enemy[];
                foreach (Enemy foeScr in PotentialFoes)
                {
                    //Ensure foe is flying
                    if (OwnerTower.OverrideTargetting || !foeScr.isFlying())
                    {
                        GameObject foe = foeScr.gameObject;
                        Vector2 FoeVector = new Vector2(Owner.transform.position.x, Owner.transform.position.y) - new Vector2(foe.transform.position.x, foe.transform.position.y);
                        float FoeDist = FoeVector.magnitude;
                        if (FoeDist < TargetDist)
                        {
                            Target = foe;
                        }
                    }
                }
            }

            if (Target)
            {
                //Get the direction to the target
                Vector2 Direction = new Vector2(Target.transform.position.x, Target.transform.position.y) -
                new Vector2(transform.position.x, transform.position.y);
                Direction = Direction.normalized;
         
                if (AttackTimer > 0)
                {
                    
                    AttackTimer -= Time.deltaTime;
                   
                }
                if (Target && Target != Owner)
                {
                    if (AttackTimer <= 0)
                    {
                        
                        float angle = Vector2.SignedAngle(gameObject.transform.position, Direction); // Returns a value between -180 and 180.
                        gameObject.transform.GetChild(0).GetComponent<testStack>().Display.rotation.z = angle + 120;
                       
                        
                        //No projectile
                        Target.GetComponent<Enemy>().takeDamage(gameObject.GetComponent<Tower>().damage);
                        

                        //GameObject Projectile = Instantiate(DroneProjectile, gameObject.transform);
                        //Projectile prj = Projectile.GetComponent<Projectile>();
                        //prj.StartPosition = gameObject.transform.position;
                        //prj.Target = Target;
                        AttackTimer += waitTime;
                    }
                }
            }
        }
    }
}
