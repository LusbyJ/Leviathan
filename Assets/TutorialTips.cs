using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialTips : MonoBehaviour
{
    public BoolSO Tutorial;
    public GameObject[] text;
    public AudioClip[] voiceLines;
    public Image character;
    public Image textbox;
    public AudioSource audioSource;

    void Start()
    {
      if(!Tutorial.Value){
        gameObject.SetActive(false);
      }
    }
    public void performRound(int RoundVal)
    {
        if (Tutorial.Value)
        {
            closeTextbox();
            gameObject.SetActive(true);
            text[RoundVal].SetActive(true);
            audioSource.clip=voiceLines[RoundVal];
            audioSource.Play(0);
        }
    }
    public void closeTextbox()
    {
        for (var i = 0; i < text.Length; i++){
            text[i].SetActive(false);
        }
        gameObject.SetActive(false);
        audioSource.Stop();
    }
}
