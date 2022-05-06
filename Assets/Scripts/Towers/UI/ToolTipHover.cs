using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;
    public string nameTip;
	public bool enemyTip;
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
        if(enemyTip){ findEnemyTip(); }
		tipToShow = tipToShow.Replace("\\n", "\n");
		ToolTipManager.OnMouseHover(tipToShow, nameTip, Input.mousePosition);
    }

    //Timer to wait so pup up doesn't immediately show up
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(waitTime);
        ShowToolTip();
    }
	
	private void findEnemyTip(){
		string name = GetComponent<Image>().sprite.name;
		if(name.Equals("smol_ground_UI_symbol1")){
			tipToShow = "Ground\nFast, but low health";
			nameTip = "Ground Grunt";
		}
		else{
			tipToShow = "Default";
			nameTip = "Default Name";
		}
	}
}