using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTutorial : MonoBehaviour, IPointerClickHandler
{
    public TutorialTips tut;
    public void OnPointerClick(PointerEventData e){
        tut.closeTextbox();
    }
 }
