using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToScene : MonoBehaviour
{
    public string scene;
    public void Click(){
      SceneManager.LoadScene(scene);
    }
    
    public void QuitGame() 
   {
       Debug.Log ("QUIT");
       Application.Quit();
   }
}
