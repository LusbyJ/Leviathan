using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public Canvas canvas;
    public bool clicked = false;
    
    public void popUp()
    {
        if (clicked == false)
        {
            clicked = true;
            canvas.enabled = true;
        }
        else if (clicked == true)
        {
            clicked = false;
            canvas.enabled = false;
        }
    }
}
