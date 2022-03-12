using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testStack : MonoBehaviour
{
    public displayObject Display;
    // Start is called before the first frame update
    void Start()
    {
      Display=gameObject.GetComponent<displayObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      Display.rotation.z++;
    }
}
