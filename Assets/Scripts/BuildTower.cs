using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildTower : MonoBehaviour
{

    public bool Placed { get; private set; }
    public BoundsInt area;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    #region Build Methods

    //Checks if selected area can be built on
    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuilding.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if(GridBuilding.current.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }

    //Build Tower on selected region
    public void Place()
    {
        Vector3Int positionInt = GridBuilding.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridBuilding.current.TakeArea(areaTemp);
    }
   
    #endregion
}
