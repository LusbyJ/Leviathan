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
	public TextMeshProUGUI enemyTipText;
    public TextMeshProUGUI enemyNameText;
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
        if(!name.Contains("District")){ enemyTip = true; }
		
		if(enemyTip){
			enemyTipText.text = tip;
			enemyNameText.text = name;
			enemyTipWindow.gameObject.SetActive(true);
			enemyTipWindow.transform.position = new Vector2(mousePos.x - enemyTipWindow.sizeDelta.x * 2, mousePos.y);
		}
		else{
			tipText.text = tip;
			nameText.text = name;
			tipWindow.gameObject.SetActive(true);
			tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x * 2, mousePos.y);
		}
    }

    private void HideTip()
    {
        Debug.Log("Exit");
		if(enemyTip){ enemyTipWindow.gameObject.SetActive(false); }
		else{ tipWindow.gameObject.SetActive(false); }
		enemyTip = false;
    }
}
