using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStack : MonoBehaviour
{
    public stackobject sprites;
    private int length=0;
    public GameObject tower;
    private Health hpScript;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        hpScript = tower.GetComponent<Health>();
        length=sprites.stack.Count;
        renderer=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      float percentage=Mathf.Clamp01((float)hpScript.health/(float)hpScript.maxHealth);
      int value=Mathf.FloorToInt(percentage*(length-1));
      Debug.Log(percentage);
      renderer.sprite=sprites.stack[value];
    }
}
