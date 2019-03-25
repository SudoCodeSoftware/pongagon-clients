using UnityEngine;
using System.Collections; 

public class Velocity : MonoBehaviour {
    public int SCap;
    
    public GameObject ten;
    public GameObject neg_ten;
    public GameObject twenty;
    public GameObject Good_Job;
    
    private int CollCount = 1;
    
    private GameObject[] GonBall;
    
    private GameObject[] GUIScore;
    
    private GameObject[] Canvas;
    
    private UnityEngine.UI.Text GUITextScore; 
    
    private int score = 0;
    
    private float mag = 3.000f;
    private float limit = 50.00f;
    private float k = 0.005f;
    
    public AudioClip Bounce;
    private AudioSource source;
    
    private GameObject Lbel; 
    
    private GameObject[] Achievments;
    private GameObject Achievment;
    private GameObject ChievObject;
    private int ChievCount = 0;
    private float ChievTimer = 0.0f;
    
    
    void Awake () {
        source = GetComponent<AudioSource>();
        source.clip = Bounce;
    }
    
    void Start() {
        Achievments = Resources.LoadAll<GameObject>("Messages") as GameObject[];
        
        PlayerPrefs.SetInt ("Player Score", score);
        GUIScore = GameObject.FindGameObjectsWithTag ("Score");
        GUITextScore = GUIScore[0].GetComponent<UnityEngine.UI.Text>();
        GUITextScore.text = score.ToString();
        GonBall = GameObject.FindGameObjectsWithTag ("Ball");
        Vector2 vel = new Vector2(0,4);
        GonBall [0].GetComponent<Rigidbody2D> ().velocity = vel * Screen.width/Screen.height;
    }

	void OnCollisionEnter2D(Collision2D coll) {
        if (Globals.Sound) {
            source.Play();
        }
        
        Canvas = GameObject.FindGameObjectsWithTag ("Canvas");
        Camera camera = GameObject.FindGameObjectsWithTag ("MainCamera")[0].GetComponent<Camera>();
        
        
		CollCount += 1;
        
		if (coll.gameObject.GetComponent<SpriteRenderer> ().sprite == GenerateSides.Bonus_Model) {
			coll.gameObject.GetComponent<SpriteRenderer> ().sprite = GenerateSides.Normal_Model;
			Globals.bonusSide = -1;
			Globals.nextBonusTime = Globals.elapsedTime + Random.Range (1000, 4000);
			score += 20;
            
            Quaternion rotation = Quaternion.Euler (0, 0, 0);
            
            Vector3 screenPoint = camera.WorldToScreenPoint(coll.gameObject.GetComponent<Transform>().position);
            Vector3 pos = new Vector3 ((screenPoint.x - Screen.width / 2.0f) * 1.2f + Screen.width / 2.0f + 5.0f, (screenPoint.y - Screen.height / 2.0f) * 1.2f + Screen.height / 2.0f - 5.0f, 0.0f);
            
            Lbel = Instantiate(twenty, pos, rotation) as GameObject;
            Lbel.GetComponent<Transform>().SetParent(Canvas[0].transform);
        
            
		} else if (coll.gameObject.GetComponent<SpriteRenderer> ().sprite == GenerateSides.Penalty_Model) {
			coll.gameObject.GetComponent<SpriteRenderer> ().sprite  = GenerateSides.Normal_Model;
			Globals.penaltySide = -1;
            Globals.PenaltyExpired = Globals.elapsedTime + 10000;
			Globals.nextPenaltyTime = Globals.elapsedTime + Random.Range (1000, 4000);
			score -= 10;
            
            Quaternion rotation = Quaternion.Euler (0, 0, 0);
            
            Vector3 screenPoint = camera.WorldToScreenPoint(coll.gameObject.GetComponent<Transform>().position);
            Vector3 pos = new Vector3 ((screenPoint.x - Screen.width / 2.0f) * 1.2f + Screen.width / 2.0f + 5.0f, (screenPoint.y - Screen.height / 2.0f) * 1.2f + Screen.height / 2.0f - 5.0f, 0.0f);
            
            Lbel = Instantiate(neg_ten, pos, rotation) as GameObject;
            Lbel.GetComponent<Transform>().SetParent(Canvas[0].transform);
        } 
        
        else {
            
            Quaternion rotation = Quaternion.Euler (0, 0, 0);
            
            Vector3 screenPoint = camera.WorldToScreenPoint(coll.gameObject.GetComponent<Transform>().position);
            Vector3 pos = new Vector3 ((screenPoint.x - Screen.width / 2.0f) * 1.2f + Screen.width / 2.0f + 5.0f, (screenPoint.y - Screen.height / 2.0f) * 1.2f + Screen.height / 2.0f - 5.0f, 0.0f);
            
            Lbel = Instantiate(ten, pos, rotation) as GameObject;
            Lbel.GetComponent<Transform>().SetParent(Canvas[0].transform);
        
			score += 10;
		}       
        
        
        PlayerPrefs.SetInt ("Player Score", score);
        GUIScore = GameObject.FindGameObjectsWithTag ("Score");
        GUITextScore = GUIScore[0].GetComponent<UnityEngine.UI.Text>();
        GUITextScore.text = score.ToString();
		GonBall = GameObject.FindGameObjectsWithTag ("Ball");
        mag = (1-k) * mag + k * limit;
		GonBall [0].GetComponent<Rigidbody2D> ().velocity = mag * GonBall [0].GetComponent<Rigidbody2D> ().velocity.normalized;
	}
    
	void Update() {
        
		GonBall = GameObject.FindGameObjectsWithTag ("Ball");
        
        ChievTimer += Time.deltaTime;
        
        Canvas = GameObject.FindGameObjectsWithTag ("Canvas");
        
        if (ChievTimer >= 7.0f)
        {
            ChievTimer = 0.0f;
            
            Quaternion rotation = Quaternion.Euler (0, 0, 0);
            
            Vector3 pos = new Vector3 (Screen.width/2.0f , Screen.height/2.0f);
            
            
            Achievment = Instantiate(Achievments[ChievCount], pos, rotation) as GameObject;
            Achievment.GetComponent<Transform>().SetParent(Canvas[0].transform);
            
            ChievCount += 1;
        
            Achievment.GetComponent<UnityEngine.UI.Text>().CrossFadeAlpha(0.0f, 2.5f, false);
            Destroy(Achievment, 2.5f);
            
        }
        
	}

}
