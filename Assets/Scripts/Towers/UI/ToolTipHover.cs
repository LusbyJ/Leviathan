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
		} else if(name.Equals("smol_flying_UI_symbol1")){
			tipToShow = "Flying\nFast, but low health";
			nameTip = "Flying Grunt";
		} else if(name.Equals("bruiser_UI_symbol")){
			tipToShow = "Ground\nSlow heavy hitter";
			nameTip = "Ground Bruiser";
		} else if(name.Equals("flying_bruiser_UI_symbol1")){
			tipToShow = "Flying\nSlow, but explodes on impact";
			nameTip = "Flying Bruiser";
		} else if(name.Equals("poisonUISymbol")){
			tipToShow = "Ground\nSlows down district attacks";
			nameTip = "Ground Poisoner";
		} else if(name.Equals("leviathan_UI_symbol1")){
			tipToShow = "Ground and Flying\nSlow moving, devastatingly strong";
			nameTip = "Leviathan";
		} else{
			tipToShow = "Default";
			nameTip = "Default Name";
		}
	}
}