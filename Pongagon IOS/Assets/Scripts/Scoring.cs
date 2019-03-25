using UnityEngine;
using System.Collections;

public class Scoring : MonoBehaviour {
    private GameObject[] GUIScore;
    private UnityEngine.UI.Text GUITextScore; 
    private int score;
    
	// Use this for initialization
    void Start () {
        score = PlayerPrefs.GetInt ("Player Score");
        GUIScore = GameObject.FindGameObjectsWithTag ("Score");
        GUITextScore = GUIScore[0].GetComponent<UnityEngine.UI.Text>();
        GUITextScore.text = score.ToString();
        
        if (PlayerPrefs.GetInt ("Player Score") > PlayerPrefs.GetInt ("High Score")) {
            PlayerPrefs.SetInt ("High Score", PlayerPrefs.GetInt ("Player Score"));
        }
        
        GUIScore = GameObject.FindGameObjectsWithTag ("HighScore");
        GUITextScore = GUIScore[0].GetComponent<UnityEngine.UI.Text>();
        GUITextScore.text = PlayerPrefs.GetInt("High Score").ToString(); 
        
        GameCentre.ReportScore(score, "Leaderboard01");
        GameCentre.ProcessLeaderboard(); 
    }    
}