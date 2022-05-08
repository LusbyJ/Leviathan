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
    public static Action<string, string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;
	
	private bool enemyTip;
	private Vector3 normalScale;
	private Vector3 enemyScale;


    // Start is called before the first frame update
    void Start()
    {
        normalScale = new Vector3(1, 1, 1);
		enemyScale = new Vector3(0.6f, 0.6f, 1);
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
        // if(!name.Contains("District")){ enemyTip = true; }
		
		tipText.text = tip;
		nameText.text = name;
		tipWindow.gameObject.SetActive(true);
		
		// if(enemyTip){
			// tipWindow.transform.localScale = enemyScale;
			// tipWindow.transform.position = new Vector2(mousePos.x - tipWindow.sizeDelta.x * 2, mousePos.y);
		// }
		// else{
			// tipWindow.transform.localScale = normalScale;
			// tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x * 2, mousePos.y);
		// }
		tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x * 2, mousePos.y);
    }

    private void HideTip()
    {
		tipWindow.gameObject.SetActive(false);
		// enemyTip = false;
    }
}
