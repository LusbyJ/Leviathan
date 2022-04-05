using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, 
    IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private Transform buildTower;
    private Vector3 mousePos;
    public Image image;
    public GameController gameController;
    public GridLayout gridLayout;           //Hexagonal grid layout
    public GameObject tower;                //Tower to be built
    public RectTransform spot;              //Spot on pop up canvas
    public static bool building = false;    //holds if building
    
    void Start() 
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            building = false;
        }

        //Grey-out towers if there is not enough credits to build
        if(tower.GetComponent<Tower>().cost > gameController.GetComponent<GameController>().credits)
        {
            var tempColor = image.color;
            tempColor.a = 0.5f;
            image.color = tempColor;
        }
        else if(tower.GetComponent<Tower>().cost <= gameController.GetComponent<GameController>().credits)
        {
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
    }

    private void Awake()
    { 
        rectTransform = GetComponent<RectTransform>();
    }

    //Detect when clicked and dragging begins
    public void OnBeginDrag(PointerEventData eventData)
    {
        //If not enough credits don't start building actions
        if (gameController.GetComponent<GameController>().credits >= tower.GetComponent<Tower>().cost)
        {
            building = true;
        }
        else
        {
            building = false;
        }
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
        if (!GridController.occupied && building == true)
        {
            cellPos.x += 1;
            Instantiate(tower, gridLayout.CellToLocal(cellPos), Quaternion.identity);
            tower.GetComponent<Tower>().cell = cellPos;
            //tower.GetComponent<Tower>().GetComponent<SpriteRenderer>().sortingOrder = cellPos.x*(-1);
            cellPos.x -= 1;
            gameController.GetComponent<GameController>().reduceCredits(tower.GetComponent<Tower>().cost);
            gridLayout.GetComponent<GridController>().occupyTile(cellPos);
        }
        else
        {
            //cellPos.x += 9;
            gridLayout.GetComponent<GridController>().ResetTile(cellPos);      
        }
        building = false;
    }

    //Detects when clicked
    public void OnPointerDown(PointerEventData eventData)
    {
     
    }
}
