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

    //Descriptions in upgrade window
    private void updateTip()
    {
        if (gameObject.name == "Slum(Clone)")
        {
            if (gameObject.GetComponent<Tower>().used)
            {
                tipToShow = " Slum District\n\tAbility in Use!";
            }
            else if (gameObject.GetComponent<Tower>().upgradeLevel == 3)
            {    
                tipToShow = " Slum District\n  Active Ability Ready!";
            }
            else
            {
                tipToShow = " Slum District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
            }
        }
        if (gameObject.name == "Sniper(Clone)")
        {
            if (gameObject.GetComponent<Tower>().used)
            {
                tipToShow = " Sniper District\n\tAbility in Use!";
            }
            else if(gameObject.GetComponent<Tower>().upgradeLevel == 3)
            {
                tipToShow = " Sniper District\n    Active Ability Ready";
            }
            else
            {
                tipToShow = " Sniper District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
            }
        }

        if (gameObject.name == "Gunner(Clone)")
        {
            if (gameObject.GetComponent<Tower>().used)
            {
                tipToShow = " Gunner District\n\tAbility in Use!";
            }
            else if(gameObject.GetComponent<Tower>().upgradeLevel == 3)
            {
                tipToShow = " Gunner District\n    Active Ability Ready!";
            }
            else
            {
                tipToShow = " Gunner District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
            }
        }

        if (gameObject.name == "DroneTower(Clone)")
        {
            if (gameObject.GetComponent<Tower>().used)
            {
                tipToShow = " Drone District\n\tAbility in Use!";
            }
            else if(gameObject.GetComponent<Tower>().upgradeLevel == 3)
            {
                tipToShow = " Drone District\n    Active Ability Ready!";
            }
            else
            {
                tipToShow = " Drone District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
            }
        }

        if (gameObject.name == "Chemical(Clone)")
        {
            if (gameObject.GetComponent<Tower>().used)
            {
                tipToShow = " Chemical District\n\tAbility in Use!";
            }
            else if (gameObject.GetComponent<Tower>().upgradeLevel == 3)
            {
                tipToShow = " Chemical District\n    Active Ability Ready!";
            }
            else
            {
                tipToShow = " Chemical District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
            }
        }

        if (gameObject.name == "Medical(Clone)")
        {
            if (gameObject.GetComponent<Tower>().used)
            {
                tipToShow = " Medical District\n\tAbility in Use!";
            }
            else if (gameObject.GetComponent<Tower>().upgradeLevel == 3)
            {
                tipToShow = " Medical District\n    Active Ability Ready!";
            }
            else
            {
                tipToShow = " Medical District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
            }
        }

        if (gameObject.name == "Missile(Clone)")
        {
            if (gameObject.GetComponent<Tower>().used)
            {
                tipToShow = " Missile District\n\tAbility in Use!";
            }
            else if(gameObject.GetComponent<Tower>().upgradeLevel == 3)
            {
                tipToShow = " Missile District\n    Active Ability Ready!";
            }
            else
            {
                tipToShow = " Missile District\n    " + gameObject.GetComponent<Tower>().upgradeCost;
            }
        }
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (DragDrop.building == false && gameObject.GetComponent<Tower>().upgradeLevel < 4)
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
        PanelManager.level = gameObject.GetComponent<Tower>().upgradeLevel;

        //Get towers health and update healthbar
        float healthStatus = gameObject.GetComponent<Health>().health / gameObject.GetComponent<Health>().maxHealth;
        
        MouseTipManager.tempHealth.transform.GetChild(1).transform.localScale = new Vector3(healthStatus, 1f, 1f);
        MouseTipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    //Timer to wait so pup up doesn't immediately show up
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(waitTime);
        ShowUpgrade();
    }
}