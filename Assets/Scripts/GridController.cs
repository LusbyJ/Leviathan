using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    private Vector3Int previousMousePos = new Vector3Int();
    public static GridController current;
    public GridLayout gridLayout;       //Hexagonal grid layout
    public Tilemap interactiveMap;      //TileMap to use for highlighting 
    public Tilemap mainMap;             //TileMap used for base tiles
    public Tile hoverTile;              //Tile to display spaces
    public Tile blockedTile;            //Tile to display blocked spaces
    public GameObject tower;            //Tower being built
    

    public static Vector2 buildTower;
    public static bool occupied = false;

    private void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Handles setting tiles during drag and drop tower building
        if (DragDrop.building == true)
        {
            Vector2 hoverPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = gridLayout.LocalToCell(hoverPos);
            cellPos.x += 9;
            
            if (mainMap.GetTile(cellPos) == blockedTile)
            {
                interactiveMap.SetTile(previousMousePos, null); //Remove old hoverTile
                interactiveMap.SetTile(cellPos, blockedTile);
                occupied = true;
            }
            else
            {
                interactiveMap.SetTile(previousMousePos, null); //Remove old hoverTile
                interactiveMap.SetTile(cellPos, hoverTile);     //Show new hoverTile
                occupied = false;
            }
                previousMousePos = cellPos;
        }      
    }
}