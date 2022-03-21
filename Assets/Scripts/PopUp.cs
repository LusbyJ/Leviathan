using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public Canvas canvas;
    public bool clicked = false;
    
    public void popUp()
    {
        if(a == false)
        {
            clicked = true;
            canvas.enabled = true;
        }
        else if(a == true)
        {
            clicked = false;
            canvas.enabled = false;
        }
    }
}
