
using UnityEngine.EventSystems;
using UnityEngine;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
    }

       public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("updating" + name + " GameObject");
    }
}
