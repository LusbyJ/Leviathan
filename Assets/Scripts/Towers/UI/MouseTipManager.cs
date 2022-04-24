using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform upgradeWindow;
    public static RectTransform tempHealth;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;


    // Start is called before the first frame update
    void Start()
    {
        HideUpgrade();
        tempHealth = upgradeWindow;
    }

    private void OnEnable()
    {
        OnMouseHover += ShowUpgrade;
        OnMouseLoseFocus += HideUpgrade;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowUpgrade;
        OnMouseLoseFocus -= HideUpgrade;
    }

    private void ShowUpgrade(string tip, Vector2 mousePos)
    {
        tipText.text = tip;
        updateHealth();
        upgradeWindow.gameObject.SetActive(true);
        upgradeWindow.transform.position = new Vector2(mousePos.x, mousePos.y + upgradeWindow.sizeDelta.y * (-2));
    }

    private void HideUpgrade()
    {
        upgradeWindow.gameObject.SetActive(false);
    }

    private void updateHealth()
    {
        upgradeWindow.transform.GetChild(1).transform.localScale = tempHealth.transform.GetChild(1).transform.localScale;
    }

}
