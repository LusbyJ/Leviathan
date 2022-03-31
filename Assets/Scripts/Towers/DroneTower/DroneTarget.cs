using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTarget : MonoBehaviour
{
    public GameObject Owner;
    private GameObject Target;
    public DroneSummoner Summoner;
    private Vector2 Speed;
    public float Accel=0.01f;
    public float Angle=0f;
    public float MaxSpeed=2f;
    public displayObject Display;
    public float AttackTime=2f;
    private float AttackTimer;
    public GameObject DroneProjectile;
    // Start is called before the first frame update
    void Start()
    {
      Target=Owner;
      Summoner=Owner.GetComponent<DroneSummoner>();
      Display=gameObject.GetComponent<displayObject>();
      AttackTimer=0;
    }

    // Update is called once per frame
    void Update()
    {
      if(!Owner){
        Destroy(this);
      }else{

        if(!Target){
          Target=Owner;
        }else{
          Vector2 TargetVec=new Vector2(Owner.transform.position.x,Owner.transform.position.y)-new Vector2(Target.transform.position.x,Target.transform.position.y);
          if(TargetVec.magnitude>Summoner.LeashRange){
              Target=Owner;
          }
        }
        if(Target==Owner){
          float TargetDist=Summoner.LeashRange;
          Enemy[] PotentialFoes = FindObjectsOfType(typeof(Enemy)) as Enemy[];
          foreach(Enemy foeScr in PotentialFoes)
          {
              if(foeScr.isFlying()){
                GameObject foe=foeScr.gameObject;
                Vector2 FoeVector=new Vector2(Owner.transform.position.x,Owner.transform.position.y)-new Vector2(foe.transform.position.x,foe.transform.position.y);
                float FoeDist=FoeVector.magnitude;
                if(FoeDist<TargetDist){
                  Target=foe;
                  TargetDist=FoeDist;
                }
              }
          }
        }
        if(Target){
          //Get the direction to the target
          Vector2 Direction=new Vector2(Target.transform.position.x,Target.transform.position.y)-
          new Vector2(transform.position.x,transform.position.y);
          Direction=Direction.normalized;
          Speed+=Direction*Accel;
          Speed*=0.999f;
          Speed=Vector2.ClampMagnitude(Speed,MaxSpeed);
          //Modify position
          transform.position=new Vector3(
            transform.position.x+Speed.x*Time.deltaTime,
            transform.position.y+Speed.y*Time.deltaTime,
            transform.position.z
          );
          Angle=Vector2.Angle(Vector2.right,Direction);
          Display.rotation=new Vector3(45,0,Angle);
          //Attacking
          if(AttackTimer>0){
            AttackTimer-=Time.deltaTime;
          }
          if(Target&&Target!=Owner){
            if(AttackTimer<=0){
              GameObject Projectile=Instantiate(DroneProjectile,gameObject.transform);
              Projectile prj=Projectile.GetComponent<Projectile>();
              prj.StartPosition=gameObject.transform.position;
              prj.Target=Target;
              AttackTimer+=AttackTime;
            }
          }
        }
      }
    }
}
