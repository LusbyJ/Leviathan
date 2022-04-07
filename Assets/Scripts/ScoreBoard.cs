using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public int newScore;
	public SortedList highScores;
	
	// Start is called before the first frame update
    void Start()
    {
        highScores = new SortedList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// public void saveScore(){ 
		// ScoreBoard.SetInt("newScore", newScore);
		// ScoreBoard.Save(); 
	// }
}
