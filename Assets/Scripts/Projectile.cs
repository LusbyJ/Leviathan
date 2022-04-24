using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 StartPosition;
    public GameObject Target;
    private float LerpVal = 0f;
    public float LerpSpd = 1;
    public float Damage = 1;
    public Animator animator;
    void Update()
    {
        if(Target)
        {
            LerpVal+=Time.deltaTime*LerpSpd;
            if(LerpVal>=1)
            {
                Target.GetComponent<Enemy>().takeDamage(Damage);
                try
                {
                    Target.GetComponent<Enemy>().takeDamage(Damage);
                }   
                catch
                {}
                Destroy(gameObject, 0.5f);
                animator.SetBool("explosion", true);
            }
            else
            {
                transform.position=Vector3.Lerp(StartPosition,Target.transform.position,LerpVal);
            }
        }
        else
        {
            Destroy(gameObject, 1);
        }
    } 
}
