using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public Animator animator;
    public static int level;
    private bool level1 = false;
    private bool level2 = false;
    private bool level3 = false;


    // Update is called once per frame
    void Update()

    {
        if (level == 3)
        {
            animator.SetBool("level1", false);
            animator.SetBool("level2", false);
            animator.SetBool("level3", true);
        }
        if (level == 2)
        {
            animator.SetBool("level1", false);
            animator.SetBool("level3", false);
            animator.SetBool("level2", true);

        }
        if (level == 1)
        {
            animator.SetBool("level1", true);
        }
  
     
    }
}
