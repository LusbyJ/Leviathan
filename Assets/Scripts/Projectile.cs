using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 StartPosition;
    public GameObject Target;
    private float LerpVal=0f;
    public float LerpSpd=1;
    public float Damage=1;
    
    void Update()
    {
      if(Target)
      {
        LerpVal+=Time.deltaTime*LerpSpd;
<<<<<<< HEAD
        if(LerpVal>=1)
        {
          Target.GetComponent<Enemy>().takeDamage(Damage);
=======
        if(LerpVal>=1){
          try{
            Target.GetComponent<Enemy>().takeDamage(Damage);
          }catch{}
>>>>>>> fe4d0147566ee099e8c88456dcb6eeec634f20f7
          Destroy(gameObject);
        }
        else
        {
          transform.position=Vector3.Lerp(StartPosition,Target.transform.position,LerpVal);
          transform.eulerAngles=new Vector3(0,0,Vector2.Angle(Vector2.right,StartPosition-Target.transform.position));
        }
      }
      else
      {
        Destroy(gameObject);
      }
    }
}
