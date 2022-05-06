using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialTips : MonoBehaviour
{
    public GameController Controller;
    public GameObject[] text;
    public Image character;
    public Image textbox;

    // Update is called once per frame
    void Update()
    {

    }
    public void performRound(int RoundVal)
    {
        if (Controller.doTutorial)
        {
            closeTextbox();
            gameObject.SetActive(true);
            text[RoundVal].SetActive(true);
        }
    }
    public void closeTextbox()
    {
        for (var i = 0; i < text.Length; i++){
            text[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
