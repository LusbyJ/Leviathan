using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, 
    IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    public GameObject tower;
    private Transform buildTower;


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
        Debug.Log("OnDrag");
    }

    //Detects when click released after dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        
        Debug.Log(eventData.position.x);
        //buildTower.position = new Vector3(eventData.position.x/canvas.scaleFactor, eventData.position.y/canvas.scaleFactor, 0);
        //Instantiate(tower, buildTower);
        Destroy(gameObject);
      
    }

    //Detects when clicked
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slot pressed");   
    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}
