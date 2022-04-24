using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class ToolTipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;
    public string nameTip;
    private float waitTime = 0.2f;

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
    
        StopAllCoroutines();
        StartCoroutine(StartTimer());
        //updateTip();
           
      
    }
    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        StopAllCoroutines();
        ToolTipManager.OnMouseLoseFocus();

    }

    //Displays upgrade window
    public void ShowToolTip()
    { 
        ToolTipManager.OnMouseHover(tipToShow, nameTip, Input.mousePosition);
    }

    //Timer to wait so pup up doesn't immediately show up
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(waitTime);
        ShowToolTip();
    }
}