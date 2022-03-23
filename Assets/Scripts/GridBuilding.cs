using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class GridBuilding : MonoBehaviour
{
    public static GridBuilding current;
    public GridLayout gridLayout;
    public Tilemap mainTilemap;
    public Tilemap tempTilemap;

    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private BuildTower temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        string tilePath = @"TileMaps\Tiles\";
        tileBases.Add(TileType.EMPTY, null);
        tileBases.Add(TileType.WHITE, Resources.Load<TileBase>(tilePath + "WHITE"));
        tileBases.Add(TileType.RED, Resources.Load<TileBase>(tilePath + "RED"));
        tileBases.Add(TileType.GREEN, Resources.Load<TileBase>(tilePath + "GREEN"));
    }

    private void Update()
    {
        if(!temp)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }

            if(!temp.Placed)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

                if(prevPos != cellPos)
                {
                    temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos 
                        + new Vector3(1f, 0f, 0f));
                    prevPos = cellPos;
                    FollowBuilding();
                }
            }
        }

        else if(Input.GetKeyDown(KeyCode.Space))
        {
            if(temp.CanBePlaced())
            {
                temp.Place();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            ClearArea();
            Destroy(temp.gameObject);
        }
    }
    #endregion

    #region Tilemap Management
    
    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }

    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }


    #endregion

    #region Building Management
    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<BuildTower>();
        FollowBuilding();
    }

    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.EMPTY);
        tempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void FollowBuilding()
    {
        ClearArea();

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i = 0; i < baseArray.Length; i++)
        {
            if(baseArray[i] == tileBases[TileType.WHITE])
            {
                tileArray[i] = tileBases[TileType.GREEN];
            }
            else
            {
                FillTiles(tileArray, TileType.RED);
                break;
            }
        }

        tempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTilemap);
        foreach(var b in baseArray)
        {
            if(b != tileBases[TileType.WHITE])
            {
                Debug.Log("Unable to place tower here");
                return false;
            }
        }
        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.EMPTY, tempTilemap);
        SetTilesBlock(area, TileType.GREEN, mainTilemap);
    }
    #endregion
}

public enum TileType
{
    EMPTY,
    WHITE,
    GREEN,
    RED
}
