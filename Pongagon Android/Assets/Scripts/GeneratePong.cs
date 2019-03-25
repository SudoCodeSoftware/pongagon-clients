using UnityEngine;
using System.Collections;

public class GeneratePong : MonoBehaviour {

	public GameObject Ball;
	private GameObject[] GonBall;
	private float Collisions;
    public AudioClip Boom;

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Portrait;
		Vector3 pos = new Vector3 (0, 0, 0);
		Quaternion rotation = Quaternion.Euler (0, 0, 0);
		Instantiate (Ball, pos, rotation);
	}

	// Update is called once per frame
	void Update () {
		float d;
		GonBall = GameObject.FindGameObjectsWithTag ("Ball");
		d = Mathf.Sqrt (Mathf.Pow (GonBall [0].GetComponent<Rigidbody2D> ().position.x, 2) + 
                        Mathf.Pow (GonBall [0].GetComponent<Rigidbody2D> ().position.y, 2));
        
        if (d > 7.5) {
            Ads.Init();
            GetComponent<AudioSource>().PlayOneShot(Boom, 0.1f); 
        }
		
        if (d > 10) {
			GameOver ();
		}
	}
	
	void GameOver() {
		GonBall = GameObject.FindGameObjectsWithTag ("Ball");
		Destroy (GonBall[0]);
		Application.LoadLevel("End");
	}

}