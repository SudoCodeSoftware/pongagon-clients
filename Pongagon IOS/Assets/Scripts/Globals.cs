using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {
	private System.Diagnostics.Stopwatch stopwatch;
    
	public static int bonusSide = -1;
	public static int penaltySide = -1;
    public static float radius;
    public static bool Music = true;
    public static bool Sound = true;
    
	public static long elapsedTime;
    public static long PenaltyExpired;
	public static long nextBonusTime;	//Time when next bonus and penalty side will be created
	public static long nextPenaltyTime;
    

	void Start () {
        bonusSide = -1;
        penaltySide = -1;
        
		stopwatch = new System.Diagnostics.Stopwatch();
		stopwatch.Start ();
		elapsedTime = stopwatch.ElapsedMilliseconds;
        
		nextBonusTime   = elapsedTime + Random.Range (1000, 4000);
		nextPenaltyTime = elapsedTime + Random.Range (1000, 4000);
        PenaltyExpired  = elapsedTime + 5000;
	}

	void Update () {
		elapsedTime = stopwatch.ElapsedMilliseconds;
	}
}