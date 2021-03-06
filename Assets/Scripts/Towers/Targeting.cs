using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public GameObject Owner;
    private GameObject Target;
    private Vector2 Speed;
    public float TargetDist;
    public displayObject Display;
    public float waitTime = 2f;
    private float AttackTimer;
    public float muzzleTime = 0.1f;
    private Tower OwnerTower;
    public stackobject IdleStack;
    public stackobject FireStack;
    public bool TargetsGround = true;
    public bool TargetsAir = true;
    public GameObject slimeBall;
    public GameObject rocket;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        Target = Owner;
        if (gameObject.name != "Medical(Clone)")
        {
            Display = gameObject.transform.GetChild(0).GetComponent<displayObject>();
        }
        AttackTimer = 0;
        OwnerTower = Owner.GetComponent<Tower>();
        damage = OwnerTower.damage;
    }

    // Update is called once per frame
    void Update()
    {
        muzzleTime = Mathf.Min(muzzleTime, waitTime / 2);
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
                if (gameObject.name != "Medical(Clone)")
                {
                    Enemy[] PotentialFoes = FindObjectsOfType(typeof(Enemy)) as Enemy[];
                    foreach (Enemy foeScr in PotentialFoes)
                    {
                        //Check Targetting
                        if (OwnerTower.OverrideTargetting ||
                        (TargetsGround && foeScr.isGround()) ||
                        (TargetsAir && foeScr.isFlying()))
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
                if (gameObject.name != "Medical(Clone)")
                {
                    if (Target && Target != Owner)
                    {
                        if (AttackTimer <= 0)
                        {

                            float angle = Vector2.SignedAngle(Vector2.right, Direction); // Returns a value between -180 and 180.

                            Display.rotation.z = angle;
                            //If Sniper shooting and active ability active increase base damage
                            if (gameObject.name == "Sniper(Clone)" && gameObject.GetComponent<Tower>().used)
                            {
                                damage += .1f;
                                Target.GetComponent<Enemy>().takeDamage(damage);
                                Display.stackObject = FireStack;
                            }
                            else if (gameObject.name != "Chemical(Clone)" && gameObject.name != "Missile(Clone)")
                            {
                                Display.stackObject = FireStack;
                                Target.GetComponent<Enemy>().takeDamage(damage);
                            }


                            else if (gameObject.name == "Chemical(Clone)")
                            {
                                GameObject Projectile = Instantiate(slimeBall, gameObject.transform);
                                Projectile prj = Projectile.GetComponent<Projectile>();
                                prj.StartPosition = gameObject.transform.position;
                                prj.Target = Target;
                                //Target.GetComponent<Enemy>().takeDamage(gameObject.GetComponent<Tower>().damage);
                            }
                            else if (gameObject.name == "Missile(Clone)")
                            {
                                GameObject Projectile = Instantiate(rocket, gameObject.transform);
                                Projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
                                Projectile prj = Projectile.GetComponent<Projectile>();
                                prj.StartPosition = gameObject.transform.position;
                                prj.Target = Target;
                                //Target.GetComponent<Enemy>().takeDamage(gameObject.GetComponent<Tower>().damage);
                            }

                            AttackTimer += waitTime;
                        }
                        if (AttackTimer < waitTime - muzzleTime)
                        {
                            Display.stackObject = IdleStack;
                        }
                    }
                    else
                    {
                        Display.stackObject = IdleStack;
                    }
                }
            }
        }
    }

    //Shoot all enemies in range, used  by chemical active ability
    public void shootEverybody()
    {
        gameObject.GetComponent<Tower>().leviathan = true;
        Enemy[] PotentialFoes = FindObjectsOfType(typeof(Enemy)) as Enemy[];
        foreach (Enemy foeScr in PotentialFoes)
        {
            GameObject foe = foeScr.gameObject;
            Vector2 FoeVector = new Vector2(Owner.transform.position.x, Owner.transform.position.y) - new Vector2(foe.transform.position.x, foe.transform.position.y);
            float FoeDist = FoeVector.magnitude;

            if (FoeDist < TargetDist && foeScr.tag == "Enemy")
            {
                if (gameObject.name == "Missile(Clone)")
                {
                    GameObject Projectile = Instantiate(rocket, gameObject.transform);
                    Projectile prj = Projectile.GetComponent<Projectile>();
                    prj.StartPosition = gameObject.transform.position;
                    prj.Target = foe;
                    foe.GetComponent<Enemy>().takeDamage(gameObject.GetComponent<Tower>().damage);
                }
                else
                {
                    GameObject Projectile = Instantiate(slimeBall, gameObject.transform);
                    Projectile prj = Projectile.GetComponent<Projectile>();
                    prj.StartPosition = gameObject.transform.position;
                    prj.Target = foe;
                    foe.GetComponent<Enemy>().takeDamage(gameObject.GetComponent<Tower>().damage);
                }

            }
        }
    }
}
