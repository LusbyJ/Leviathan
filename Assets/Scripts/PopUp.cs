using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public Canvas canvas;
    public bool clicked;

    void Start() { }

    public void popUp()
    {
        Debug.Log(clicked);
        if (clicked == true)
        {
            canvas.enabled = true;
            clicked = false;
        }
        else if (clicked == false)
        {
            clicked = clicked = true; 
            canvas.enabled = false;
        }
    }
}
