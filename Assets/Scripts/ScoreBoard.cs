using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
	public FloatSO ScoreSO;
	public TMP_Text[] scoreTexts;
	public TMP_Text[] nameTexts;
	public GameObject scorePopup;
	public TMP_InputField input;
	public Button button;
	public int[] highScores;
	public string[] scoreNames;
	
	private int scoreInd;
	
    void Awake()
    {
        scorePopup.SetActive(false);
		highScores = new int[10]{   PlayerPrefs.GetInt("1_Score"),
									PlayerPrefs.GetInt("2_Score"),
									PlayerPrefs.GetInt("3_Score"),
									PlayerPrefs.GetInt("4_Score"),
									PlayerPrefs.GetInt("5_Score"),
									PlayerPrefs.GetInt("6_Score"),
									PlayerPrefs.GetInt("7_Score"),
									PlayerPrefs.GetInt("8_Score"),
									PlayerPrefs.GetInt("9_Score"),
									PlayerPrefs.GetInt("10_Score")
		};
		
		scoreNames = new string[10]{PlayerPrefs.GetString("1_Name"),
									PlayerPrefs.GetString("2_Name"),
									PlayerPrefs.GetString("3_Name"),
									PlayerPrefs.GetString("4_Name"),
									PlayerPrefs.GetString("5_Name"),
									PlayerPrefs.GetString("6_Name"),
									PlayerPrefs.GetString("7_Name"),
									PlayerPrefs.GetString("8_Name"),
									PlayerPrefs.GetString("9_Name"),
									PlayerPrefs.GetString("10_Name")
		};		
		updateScores();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 10; i++){
			scoreTexts[i].text = "" + highScores[i];
			nameTexts[i].text = scoreNames[i];
		}
    }
	
	private void updateScores(){
		bool added = false;
		int oldScore = 0; 
		int newScore = 0;
		string oldName = "";
		string newName = "";
		for(int i = 0; i < 10; i++){
			if(added){
				newScore = oldScore;
				newName = oldName;
				oldScore = highScores[i];
				oldName = scoreNames[i];
				highScores[i] = newScore;
				scoreNames[i] = newName;
				PlayerPrefs.SetInt(((i+1) + "_Score"), newScore);
				PlayerPrefs.SetString(((i+1) + "_Name"), newName);
			} else{
				if(ScoreSO.Value > highScores[i]){
					oldScore = highScores[i];
					oldName = scoreNames[i];
					highScores[i] = (int)ScoreSO.Value;
					scoreNames[i] = "";
					PlayerPrefs.SetInt(((i+1) + "_Score"), (int)ScoreSO.Value);
					PlayerPrefs.SetString(((i+1) + "_Name"), "");
					scoreInd = i;
					added = true;
					scorePopup.SetActive(true);
					button.onClick.AddListener(SaveNewName);
				}
			}
		}
		PlayerPrefs.Save();
	}
	
	void SaveNewName(){
		string newName = input.text;
		if(scoreInd > -1){ 
			scoreNames[scoreInd] = newName;
			PlayerPrefs.SetString(((scoreInd+1) + "_Name"), newName);
			PlayerPrefs.Save();
		}
		scorePopup.SetActive(false);
	}
}
