using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, 
    IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    public GridLayout gridLayout;       //Hexagonal grid layout
    private RectTransform rectTransform;
    public GameObject tower;
    private Transform buildTower;
    public static bool building = false;
    private Vector3 mousePos;

    void Start() { }

    private void Awake()
    { 
        rectTransform = GetComponent<RectTransform>();
    }

    //Detect when clicked and dragging begins
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    //Handles position while being dragged
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //Detects when click released after dragging
    public void OnEndDrag(PointerEventData eventData)
    {

        //destroy object being dragged 
        Destroy(gameObject);
       
        //Instantiate a new tower at end of drag location
        Vector2 hoverPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3Int cellPos = gridLayout.LocalToCell(hoverPos);

        Debug.Log(cellPos);
        cellPos.x += 1;

        //Instantiate(tower, hoverPos, Quaternion.identity);
        Instantiate(tower, gridLayout.CellToLocal(cellPos), Quaternion.identity);
       //tower.transform.position = cellPos;
        Debug.Log(tower.transform.position);
        building = false;
    }

    //Detects when clicked
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slot pressed");
        building = true;
    }
}
