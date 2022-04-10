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
    public static Tile emptyTile;              //Tile with no tower
    public GameObject tower;            //Tower being built
    public static List<Vector3Int> towerList = new List<Vector3Int>();
    public static List<Vector3Int> deadTowers = new List<Vector3Int>();
    

    public static Vector2 buildTower;
    public static bool occupied = false;

    private void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Vector3Int pos in deadTowers)
        {
            occupyTile(pos);
        }
        Vector2 hoverPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = gridLayout.LocalToCell(hoverPos);
        
        //Handles setting tiles during drag and drop tower building
        if (DragDrop.building == true)
        {        
            if (towerList.Contains(cellPos) == true || deadTowers.Contains(cellPos) == true)
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
        else
        {
            interactiveMap.SetTile(cellPos, null);
        }
    }
    
    public void ResetTile(Vector3Int cellPos)
    {
        interactiveMap.SetTile(cellPos, null);     
        occupied = false;
    }

    public void removeTile(Vector3Int cellPos)
    {
        mainMap.SetTile(cellPos, emptyTile);
    }

    public void occupyTile(Vector3Int cellPos)
    {
        mainMap.SetTile(cellPos, blockedTile);
    }
}