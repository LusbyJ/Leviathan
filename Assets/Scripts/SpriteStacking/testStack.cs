using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testStack : MonoBehaviour
{
    public displayObject Display;
    public stackobject baseStack;
    public stackobject fireStack;
    // Start is called before the first frame update
    void Start()
    {
      Display=gameObject.GetComponent<displayObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      Display.rotation.z++;
      if(Input.GetKey("space")){
        Display.stackObject=fireStack;
      }else{
        Display.stackObject=baseStack;
      }
    }
}
