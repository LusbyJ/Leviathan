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

/*    void Update()
    {
        updateTip();
       
    }*/

    //Descriptions in upgrade window
    private void updateTip()
    {
        var a = gameObject.GetComponent<Tower>();
        if (gameObject.name == "Slum(Clone)")
        {
            if (a.used || a.leviathan)
            {
                tipToShow = " Slum District";
            }
            else if (a.upgradeLevel == 3)
            {    
                tipToShow = " Slum District\n\tAbility Ready!";
            }
            else
            {
                tipToShow = " Slum District\n    " + a.upgradeCost + "  +H +Mitigation";
            }
        }
        if (gameObject.name == "Sniper(Clone)")
        {
            if (a.used || a.leviathan)
            {
                tipToShow = " Sniper District\n";
            }
            else if(a.upgradeLevel == 3)
            {
                tipToShow = " Sniper District\n\tAbility Ready";
            }
            else
            {
                tipToShow = " Sniper District\n    " + a.upgradeCost + "  +D +Speed";
            }
        }

        if (gameObject.name == "Gunner(Clone)")
        {
            if (a.used || a.leviathan)
            {
                tipToShow = " Gunner District\n";
            }
            else if(a.upgradeLevel == 3)
            {
                tipToShow = " Gunner District\n\tAbility Ready!";
            }
            else
            {
                tipToShow = " Gunner District\n    " + a.upgradeCost + "  +H +Speed";
            }
        }

        if (gameObject.name == "DroneTower(Clone)")
        {
            if (a.used || a.leviathan)
            {
                tipToShow = " Drone District\n";
            }
            else if(a.upgradeLevel == 3)
            {
                tipToShow = " Drone District\n\tAbility Ready!";
            }
            else
            {
                tipToShow = " Drone District\n    " + a.upgradeCost + "  +H +Drone";
            }
        }

        if (gameObject.name == "Chemical(Clone)")
        {
            if (a.used || a.leviathan)
            {
                tipToShow = " Chemical District\n";
            }
            else if (a.upgradeLevel == 3)
            {
                tipToShow = " Chemical District\n\tAbility Ready!";
            }
            else
            {
                tipToShow = " Chemical District\n    " + a.upgradeCost + "  +R +Speed";
            }
        }

        if (gameObject.name == "Medical(Clone)")
        {
            if (a.used || a.leviathan)
            {
                tipToShow = " Medical District\n";
            }
            else if (a.upgradeLevel == 3)
            {
                tipToShow = " Medical District\n\tAbility Ready!";
            }
            else
            {
                tipToShow = " Medical District\n    " + a.upgradeCost + " +H +Speed";
            }
        }

        if (gameObject.name == "Missile(Clone)")
        {
            if (a.used || a.leviathan)
            {
                tipToShow = " Missile District\n";
            }
            else if(a.upgradeLevel == 3)
            {
                tipToShow = " Missile District\n\tAbility Ready!";
            }
            else
            {
                tipToShow = " Missile District\n    " + a.upgradeCost + " +Range +Speed";
            }
        }
        ShowUpgrade();
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (DragDrop.building == false && gameObject.GetComponent<Tower>().upgradeLevel < 4)
        {
            StopAllCoroutines();
            StartCoroutine(StartTimer());
            InvokeRepeating("updateTip", 0.1f, 0.2f);
        }
    }
    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        StopAllCoroutines();
        MouseTipManager.OnMouseLoseFocus();
        CancelInvoke();
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