using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class GameCentre : MonoBehaviour {
    
    
    static ILeaderboard m_Leaderboard;
    
    
    void Start () {
        Social.localUser.Authenticate (ProcessAuthentication);
        DoLeaderboard();
    }

    // This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    static public void ProcessAuthentication (bool success) {
        if (success) {
            Debug.Log ("Authenticated");

            // Request loaded achievements, and register a callback for processing them
        }
        else
            Debug.Log ("Failed to authenticate");
    }
    
    static public void ProcessLeaderboard (){
        Social.LoadScores("Leaderboard01", scores => {
			if (scores.Length > 0) {
				Debug.Log ("Got " + scores.Length + " scores");
				string myScores = "Leaderboard:\n";
				foreach (IScore score in scores)
					myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";
				Debug.Log (myScores);
			}
			else
				Debug.Log ("No scores loaded");
		  });
    }
    
    static public void ReportScore (long score, string leaderboardID) {
		Debug.Log ("Reporting score " + score + " on leaderboard " + leaderboardID);
		Social.ReportScore (score, leaderboardID, success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
        
    }
    
    void DoLeaderboard () {
    	m_Leaderboard = Social.CreateLeaderboard();
    	m_Leaderboard.id = "Leaderboard01";  // YOUR CUSTOM LEADERBOARD NAME
    	m_Leaderboard.LoadScores(result => DidLoadLeaderboard(result));
	}
    
    void DidLoadLeaderboard (bool result) {
    	Debug.Log("Received " + m_Leaderboard.scores.Length + " scores");
    	foreach (IScore score in m_Leaderboard.scores) {
        	Debug.Log(score);
		}
	}
    
    public void ShowBoard () {
        Social.ShowLeaderboardUI();
    }
}