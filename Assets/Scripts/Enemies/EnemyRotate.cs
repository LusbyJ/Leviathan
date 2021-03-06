using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    public stackobject sprites;
    private int length;
    public GameObject enemy;
    private  Enemy escript;
    private SpriteRenderer spriterenderer;
    private float threshold=0.5f;
    // Start is called before the first frame update
    void Start()
    {
      length=sprites.stack.Count;
      escript=enemy.GetComponent<Enemy>();
      spriterenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 dir=escript.getDirection();
      int value=0;
      if(dir.x>threshold){
        value=2;
      }else if(dir.y<-threshold){
        value=0;
      }else if(dir.x<-threshold){
        value=6;
      }else if(dir.y>threshold){
        value=4;
      }else if(dir.x>=0&&dir.y<=0){
        value=1;
      }else if (dir.x>=0&&dir.y>=0){
        value=3;
      }else if (dir.x<=0&&dir.y>=0){
        value=5;
      }else{
        value=7;
      }
      spriterenderer.sprite=sprites.stack[value];
    }
}
