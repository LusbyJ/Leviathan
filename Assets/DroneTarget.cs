using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTarget : MonoBehaviour
{
    public GameObject Owner;
    private GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
      Target=Owner;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
