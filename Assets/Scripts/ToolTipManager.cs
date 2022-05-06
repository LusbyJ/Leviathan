using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public TextMeshProUGUI nameText;
    public RectTransform tipWindow;
	public RectTransform enemyTipWindow;
    public static Action<string, string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;
	
	private bool enemyTip;


    // Start is called before the first frame update
    void Start()
    {
        HideTip();
    }

    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    private void ShowTip(string tip, string name, Vector2 mousePos)
    {
        tipText.text = tip;
        nameText.text = name;
		
		if(!name.Contains("District")){ enemyTip = true; }
		else{ enemyTip = false; }
		
		if(enemyTip){
			enemyTipWindow.gameObject.SetActive(true);
			enemyTipWindow.transform.position = new Vector2(mousePos.x - enemyTipWindow.sizeDelta.x * 2, mousePos.y);
		}
		else{
			tipWindow.gameObject.SetActive(true);
			tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x * 2, mousePos.y);
		}
    }

    private void HideTip()
    {
        if(enemyTip){ enemyTipWindow.gameObject.SetActive(false); }
		else{ tipWindow.gameObject.SetActive(false); }
    }
}
