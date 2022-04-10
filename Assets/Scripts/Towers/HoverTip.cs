using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;
    private float waitTime = 0.2f;

    void Start()
    {
        tipToShow = "" + gameObject.GetComponent<Tower>().upgradeCost;
    }

    void Update()
    {
        tipToShow = "" + gameObject.GetComponent<Tower>().upgradeCost;
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (DragDrop.building == false && gameObject.GetComponent<Tower>().upgradeLevel < 3)
        {
            StopAllCoroutines();
            StartCoroutine(StartTimer());
        }
    }
    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        StopAllCoroutines();
        MouseTipManager.OnMouseLoseFocus();
    }
    
    //Displays upgrade window
    public void ShowUpgrade()
    {
        MouseTipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    //Timer to wait so pup up doesn't immediately show up
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(waitTime);

        ShowUpgrade();
    }
}
