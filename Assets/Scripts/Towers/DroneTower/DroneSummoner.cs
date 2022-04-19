using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSummoner : MonoBehaviour
{
    public GameObject Drone;
    private float SummonDistance = 0.2f;
    public int DroneCount = 3;
    public float LeashRange = 1.2f;
    public static List<GameObject> roundDrones = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < DroneCount; i++)
        {
            SummonDrone();
        }
    }

   
    void Update()
    {
        //disables drone factory
        if(gameObject.GetComponent<Tower>().used == false)
        {
            CancelInvoke();
        }
    }

    public void SummonDrone()
    {
        Vector2 SummonPosition = Random.insideUnitCircle.normalized * SummonDistance;
        Vector3 position = new Vector3(transform.position.x + SummonPosition.x, transform.position.y + SummonPosition.y, transform.position.z);
        GameObject SummonedDrone = Instantiate(Drone, transform);
        SummonedDrone.transform.position = position;
        SummonedDrone.GetComponent<DroneTarget>().Owner = gameObject;
    }

    //Initiate Drone Factory to make drones every 15 seconds
    public void extraDrone()
    {
        InvokeRepeating("DroneFactory", 0.0f, 15.0f);
    }

    //Remove Drones
    public void resetDrones()
    {
        StopCoroutine("DroneFactory");
        foreach (GameObject extraDrone in roundDrones)
        {
            if (extraDrone != null)
            {
                Destroy(extraDrone);
            }
        }
        roundDrones.Clear();
    }

    //Summon an extra drone for this round
    private void DroneFactory()
    {
        Vector2 SummonPosition = Random.insideUnitCircle.normalized * SummonDistance;
        Vector3 position = new Vector3(transform.position.x + SummonPosition.x, transform.position.y + SummonPosition.y, transform.position.z);
        GameObject activeDrone = Instantiate(Drone, transform);
        roundDrones.Add(activeDrone);
        activeDrone.transform.position = position;
        activeDrone.GetComponent<DroneTarget>().Owner = gameObject;
    }
}
