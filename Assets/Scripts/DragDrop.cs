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
    public GridLayout gridLayout;           //Hexagonal grid layout
    public GameObject tower;                //Tower to be built
    public RectTransform spot;              //Spot on pop up canvas
    public static bool building = false;    //holds if building
    
    void Start() { }

    private void Awake()
    { 
        rectTransform = GetComponent<RectTransform>();
    }

    //Detect when clicked and dragging begins
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
    }

    //Handles position while being dragged
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //Detects when click released after dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        //Instantiate a new tower at end of drag location
        if (!GridController.occupied)
        {
            Vector2 hoverPos = Camera.main.ScreenToWorldPoint(eventData.position);
            Vector3Int cellPos = gridLayout.LocalToCell(hoverPos);
            cellPos.x += 1;
            
            //destroy object being dragged 
            Destroy(gameObject);
            Instantiate(tower, gridLayout.CellToLocal(cellPos), Quaternion.identity);
        }
        building = false;
    }

    //Detects when clicked
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Slot pressed");
        building = true;
    }
}
