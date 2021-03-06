using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,
    IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private Transform buildTower;
    private Vector3 mousePos;
    public Image image;
    public GameController gameController;
    public GridLayout gridLayout;           //Hexagonal grid layout
    public GameObject tower;                //Tower to be built
    public GameObject range;                //Range of tower indicator
    public static bool building = false;    //holds if building



    void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            building = false;
        }

        //Grey-out towers if there is not enough credits to build
        if (tower.GetComponent<Tower>().cost > gameController.GetComponent<GameController>().credits)
        {
            var tempColor = image.color;
            tempColor.a = 0.2f;
            image.color = tempColor;
        }
        else if (tower.GetComponent<Tower>().cost <= gameController.GetComponent<GameController>().credits)
        {
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
    }




    //Detect when clicked and dragging begins
    public void OnBeginDrag(PointerEventData eventData)
    {
        //If not enough credits don't start building actions
        if (gameController.GetComponent<GameController>().credits >= tower.GetComponent<Tower>().cost)
        {
            building = true;
            GameController gcontrol = gameController.GetComponent<GameController>();
            gcontrol.towerPlaced = true;
            //Get the towers targetDistance, Instantiate target range indicator
            if (tower.name == "DroneTower")
            {
                //Get the towers targetDistance, Instantiate target range indicator
                float d = 1.5f * 2;
                Vector3 targetDistance = new Vector3(d, d, 0);
                range.transform.localScale = targetDistance;
            }
            else if (tower.name == "Nuclear")
            {
                //Set the towers targetDistance, Instantiate target range indicator
                float d = 2f;
                Vector3 targetDistance = new Vector3(d, d, 0);
                range.transform.localScale = targetDistance;
            }
            else if (tower.name == "Slum" || tower.name == "Medical")
            {
                //Set the towers targetDistance, Instantiate target range indicator
                float d = 0.55f;
                Vector3 targetDistance = new Vector3(d, d, 0);
                range.transform.localScale = targetDistance;

            }
            else
            {
                //Get the towers targetDistance, Instantiate target range indicator
                float d = tower.GetComponent<Targeting>().TargetDist * 2;
                Vector3 targetDistance = new Vector3(d, d, 0);
                range.transform.localScale = targetDistance;
            }
            Instantiate(range);
        }
        else
        {
            building = false;

        }
    }

    //Detects when click released after dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 hoverPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3Int cellPos = gridLayout.LocalToCell(hoverPos);

        if (!GridController.occupied && building == true)
        {
            GameController gcontrol = gameController.GetComponent<GameController>();
            gcontrol.reduceCredits(tower.GetComponent<Tower>().cost);
            //Instantiate a new tower at end of drag location
            Vector3Int placePosition = cellPos;
            placePosition.x = cellPos.x + 1;
            tower.GetComponent<Tower>().cell = cellPos;
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.buildTower);
            Instantiate(tower, gridLayout.CellToLocal(placePosition), Quaternion.identity);

            //Add tower location to towerList, reduce credits, occupy tile
            GridController.towerList.Add(cellPos);
            GridController.occupied = false;
        }
        else
        {
            gridLayout.GetComponent<GridController>().ResetTile(cellPos);
        }
        building = false;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
    }

    public void OnDrag(PointerEventData data)
    {
    }
}
