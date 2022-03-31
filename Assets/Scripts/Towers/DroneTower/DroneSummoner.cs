using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSummoner : MonoBehaviour
{
    public GameObject Drone;
    private float SummonDistance=0.2f;
    public int DroneCount=3;
    public float LeashRange=1.2f;
    // Start is called before the first frame update
    void Start()
    {
      for(var i=0;i<DroneCount;i++){
        SummonDrone();
      }
    }

    public void SummonDrone(){
        Vector2 SummonPosition=Random.insideUnitCircle.normalized*SummonDistance;
        Vector3 position=new Vector3(transform.position.x+SummonPosition.x,transform.position.y+SummonPosition.y,transform.position.z);
        GameObject SummonedDrone=Instantiate(Drone,transform);
        SummonedDrone.transform.position=position;
        SummonedDrone.GetComponent<DroneTarget>().Owner=gameObject;
    }
}
