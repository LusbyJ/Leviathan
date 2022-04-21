using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;
    private float waitTime = 0.2f;


    void Start()
    {
        tipToShow = " " + gameObject.GetComponent<Tower>().name + " District\n    " +
            gameObject.GetComponent<Tower>().cost;
    }

    void Update()
    {
        updateTip();
       
    }

    private void updateTip()
    {
        if (gameObject.name == "Slum(Clone)")
        {
            tipToShow = " Slum District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
        }

        if (gameObject.name == "Sniper(Clone)")
        {
            tipToShow = " Sniper District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
        }

        if (gameObject.name == "Gunner(Clone)")
        {
            tipToShow = " Gunner District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
        }

        if (gameObject.name == "DroneTower(Clone)")
        {
            tipToShow = " Drone District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
        }

        if (gameObject.name == "Chemical(Clone)")
        {
            tipToShow = " Chemical District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
        }

        if (gameObject.name == "Medical(Clone)")
        {
            tipToShow = " Medical District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
        }

        if (gameObject.name == "Missile(Clone)")
        {
            tipToShow = " Missile District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
        }
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
        float healthStatus = gameObject.GetComponent<Health>().health / gameObject.GetComponent<Health>().maxHealth;
        //MouseTipManager.tempHealth = new Vector3(healthStatus, 1f, 1f);
        //health.transform.localScale = new Vector3(healthStatus,1f,1f);
        MouseTipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    //Timer to wait so pup up doesn't immediately show up
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(waitTime);

        ShowUpgrade();
    }
}