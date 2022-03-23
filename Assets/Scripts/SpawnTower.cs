using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{

    public GameObject tower;

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(tower, new Vector3(cursorPos.x, cursorPos.y, 0), Quaternion.identity);
        }
    }
}
