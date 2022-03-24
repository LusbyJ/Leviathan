using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    public static GridController current;
    public GridLayout gridLayout;       //Hexagonal grid layout
    public Tilemap interactiveMap;      //TileMap to use for highlighting 
    public Tilemap terrain;             //TileMap used for base tiles
    public Tile hoverTile;              //Tile to display open or occupied tiles   
    public GameObject tower;
    

    private Vector3Int previousMousePos = new Vector3Int();
    public static Vector2 buildTower;
    public static Transform tile;

    private void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (DragDrop.building == true)
        {


            Vector2 hoverPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = gridLayout.LocalToCell(hoverPos);
           
            

            /*//If grid clicked build tower
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(tower);
                tower.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                            + new Vector3(1f, 0f, 0f));
            }

            else
            {*/
                interactiveMap.SetTile(previousMousePos, null); //Remove old hoverTile
                interactiveMap.SetTile(cellPos, hoverTile);     //Show new hoverTile
             
                previousMousePos = cellPos;
            //}
        }
       
    }
}