using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MouseTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform upgradeWindow;
    public static RectTransform tempHealth;
    public Image Icon1;
    public Image Icon2;
    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    public Sprite damageMitigation;
    public Sprite damageUpgrade;
    public Sprite healSpeed;
    public Sprite healthUpgrade;
    public Sprite drone;
    public Sprite rangeUpgrade;
    public Sprite fireSpeed;
    public Sprite empty;


    // Start is called before the first frame update
    void Start()
    {
        HideUpgrade();
        tempHealth = upgradeWindow;
    }

    //Changes upgrade icons
    public void setIcon(int icon1, int icon2)
    {
        if (icon1 == 1)
        {
            Icon1.sprite = fireSpeed;
            var tempColor = Icon1.color;    
            tempColor.a = 1f;
            Icon1.color = tempColor;
            
        }
        else if (icon1 == 2)
        {
            Icon1.sprite = drone;
            var tempColor = Icon1.color;
            tempColor.a = 1f;
            Icon1.color = tempColor;
        }
        else if (icon1 == 3)
        {
            Icon1.sprite = healSpeed;
            var tempColor = Icon1.color;
            tempColor.a = 1f;
            Icon1.color = tempColor;
        }
        else if (icon1 == 4)
        {
            Icon1.sprite = damageMitigation;
            var tempColor = Icon1.color;
            tempColor.a = 1f;
            Icon1.color = tempColor;
        }

        if (icon2 == 1)
        {
            Icon2.sprite = healthUpgrade;
            var tempColor = Icon2.color;
            tempColor.a = 1f;
            Icon2.color = tempColor;
        }
        else if (icon2 == 2)
        {
            Icon2.sprite = rangeUpgrade;
            var tempColor = Icon2.color;
            tempColor.a = 1f;
            Icon2.color = tempColor;
        }
        else if (icon2 == 3)
        {
            Icon2.sprite = damageUpgrade;
            var tempColor = Icon2.color;
            tempColor.a = 1f;
            Icon2.color = tempColor;
        }
        else
        {
            var tempColor1 = Icon1.color;
            var tempColor2 = Icon2.color;
            tempColor1.a = 0f;
            tempColor2.a = 0f;
            Icon1.color = tempColor1;
            Icon2.color = tempColor2;
        }
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
