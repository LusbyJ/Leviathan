using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, 
    IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private Transform buildTower;
    private Vector3 mousePos;
    public GameController gameController;
    public GridLayout gridLayout;           //Hexagonal grid layout
    public GameObject tower;                //Tower to be built
    public RectTransform spot;              //Spot on pop up canvas
    public static bool building = false;    //holds if building
    
    void Start() 
    {
        
    }

    private void Awake()
    { 
        rectTransform = GetComponent<RectTransform>();
    }

    //Detect when clicked and dragging begins
    public void OnBeginDrag(PointerEventData eventData)
    {
        building = true;
    }

    //Handles position while being dragged
    public void OnDrag(PointerEventData eventData)
    {
        
    }

    //Detects when click released after dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 hoverPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3Int cellPos = gridLayout.LocalToCell(hoverPos);

        //Instantiate a new tower at end of drag location
        if (!GridController.occupied)
        {
            cellPos.x += 1;
            Instantiate(tower, gridLayout.CellToLocal(cellPos), Quaternion.identity);
            cellPos.x += 8;
            gameController.GetComponent<GameController>().reduceCredits(tower.GetComponent<Tower>().cost);
            gridLayout.GetComponent<GridController>().occupyTile(cellPos);
        }
        else
        {
            cellPos.x += 9;
            gridLayout.GetComponent<GridController>().ResetTile(cellPos);      
        }
        building = false;
    }

    //Detects when clicked
    public void OnPointerDown(PointerEventData eventData)
    {
     
    }
}
