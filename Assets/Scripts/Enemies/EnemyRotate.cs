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
      float percentage=Mathf.Clamp01(Vector3.Angle(Vector3.down,escript.getDirection())/360f);
      int value=Mathf.FloorToInt(percentage*(length-1));
      spriterenderer.sprite=sprites.stack[value];
    }
}
