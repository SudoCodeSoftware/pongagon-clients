using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenerateSides : MonoBehaviour {
    
    public GameObject Sides;
    
	private GameObject[] gonSides;
    private GameObject[] ScoreNotifications;
    
	public int NumberSides;
	public float mag;
	public int Sense;
	private float GonAngle;
    private GameObject NGon;
	public int ptype;
    
    public static Sprite Normal_Model;
    public static Sprite Penalty_Model;
    public static Sprite Bonus_Model;
    public static GameObject Good_Job;
    
    private float touchStartAngle;
    private float gonStartAngle;
    private float Notification_x;
    private float Notification_y;
    private Vector3 Notification_pos;
    
    private const float moveSpeed = 3.0f;

    public void Start () {
		float angle;
		float y;
		float x;
		float rotate;
        
        Normal_Model = Resources.Load<Sprite>("gon");
        Bonus_Model = Resources.Load<Sprite>("2x");
        Penalty_Model = Resources.Load<Sprite>("gonout");
        
		angle = 2.0f * Mathf.PI / NumberSides;
        Debug.Log(mag);
        Globals.radius = mag;
        for (int i = 0; i < NumberSides; i++) {
			x = mag * Mathf.Cos (angle * i + Mathf.PI / 2.0f);
			y = mag * Mathf.Sin (angle * i + Mathf.PI / 2.0f);
			rotate = angle * i * 180.0f / Mathf.PI;
			Quaternion rotation = Quaternion.Euler (0, 0, rotate);
			Vector3 pos = new Vector3 (x, y, 0.0f);
			NGon = Instantiate (Sides, pos, rotation) as GameObject;
            NGon.transform.localScale *= 0.75f;
		}

		gonSides = new GameObject[NumberSides];
		gonSides = GameObject.FindGameObjectsWithTag ("Block");
	}

	void Update () {
		float angle;
		float y;
		float x;
		float rotate;
        
        ScoreNotifications = new GameObject[20];
        ScoreNotifications = GameObject.FindGameObjectsWithTag ("ScoreNot");
        
        for (int i = 0; i < ScoreNotifications.Length; i++) {
            Notification_x = ScoreNotifications[i].gameObject.GetComponent<Transform>().position.x;
            Notification_y = ScoreNotifications[i].gameObject.GetComponent<Transform>().position.y;

            Notification_x -= Screen.width / 2;
            Notification_x *= 1.002f;
            Notification_x += Screen.width / 2;
            Notification_y -= Screen.height / 2;
            Notification_y *= 1.002f;
            Notification_y += Screen.height / 2;
            
            Notification_pos = new Vector3 (Notification_x, Notification_y);
            
            ScoreNotifications[i].gameObject.GetComponent<Transform>().position = Notification_pos;
            ScoreNotifications[i].GetComponent<UnityEngine.UI.Text>().CrossFadeAlpha(0.0f, 0.5f, false);
            
            Destroy(ScoreNotifications[i].gameObject, 2.5f);    
            
   
            }
        
        if (Globals.bonusSide == -1 && Globals.elapsedTime >= Globals.nextBonusTime) 
        {
			Globals.bonusSide = Random.Range (0, 7);
			while (Globals.bonusSide == Globals.penaltySide) {
				Globals.bonusSide = Random.Range (0, 7);
			}

            gonSides[Globals.bonusSide].gameObject.GetComponent<SpriteRenderer>().sprite = Bonus_Model;
            
		}
        if (Globals.penaltySide != -1 && Globals.elapsedTime >= Globals.PenaltyExpired) {
            gonSides[Globals.penaltySide].gameObject.GetComponent<SpriteRenderer>().sprite = Normal_Model;
            
			Globals.penaltySide = -1;
            Globals.PenaltyExpired = Globals.elapsedTime + 10000;
            Globals.nextPenaltyTime = Globals.elapsedTime + Random.Range (1000, 4000);

		}
        
        if (Globals.penaltySide == -1 && Globals.elapsedTime >= Globals.nextPenaltyTime) {
			Globals.penaltySide = Random.Range (0, 7);

			while (Globals.penaltySide == Globals.bonusSide) {
				Globals.penaltySide = Random.Range (0, 7);
			}

            gonSides[Globals.penaltySide].gameObject.GetComponent<SpriteRenderer>().sprite = Penalty_Model;
		}


		if (ptype == 0) {			//Keyboard
			float moveHorizontal;

			moveHorizontal = Input.GetAxis ("Horizontal");
			GonAngle += -moveHorizontal / Sense;
			angle = 2.0f * Mathf.PI / NumberSides;
		} 

		else if (ptype == 1) {		//Touch
			angle = 2.0f * Mathf.PI / NumberSides;

			for (int j=0; j < Input.touchCount; ++j) {
				Touch touch = Input.GetTouch (j);
                
				switch (touch.phase) {
					case TouchPhase.Began:
						touchStartAngle = Mathf.Atan2 (touch.position.y - Screen.height / 2, touch.position.x - Screen.width / 2);
						gonStartAngle = GonAngle;
						break;

					case TouchPhase.Moved:
						float GonAngleTarget = ((Mathf.Atan2 (touch.position.y - Screen.height / 2, touch.position.x - Screen.width / 2) 
						                         - touchStartAngle
						                         + gonStartAngle
						                         + Mathf.PI) 
						                         % (2 * Mathf.PI))
												 - Mathf.PI;
						
						GonAngle += Mathf.Clamp(((GonAngleTarget - GonAngle + Mathf.PI) % (2 * Mathf.PI) + 2 * Mathf.PI) % (2 * Mathf.PI) - Mathf.PI, -Time.deltaTime * moveSpeed, Time.deltaTime * moveSpeed);
                        touchStartAngle = Mathf.Atan2 (touch.position.y - Screen.height / 2, touch.position.x - Screen.width / 2);
				        gonStartAngle = GonAngle;
                        
						break;
				}
			}
		}

		else {		//Mouse
			angle = 2.0f * Mathf.PI / NumberSides;

			Vector2 mousePos = Input.mousePosition;
				
			if (Input.GetMouseButtonDown(0)) {
				touchStartAngle = Mathf.Atan2 (mousePos.y - Screen.height / 2, mousePos.x - Screen.width / 2);
				gonStartAngle = GonAngle;
			}

			else if (Input.GetMouseButton(0)) {
				float GonAngleTarget = ((Mathf.Atan2 (mousePos.y - Screen.height / 2, mousePos.x - Screen.width / 2) 
				            - touchStartAngle
				            + gonStartAngle
				            + Mathf.PI) 
							% (2 * Mathf.PI))
							- Mathf.PI;

				GonAngle += Mathf.Clamp(((GonAngleTarget - GonAngle + Mathf.PI) % (2 * Mathf.PI) + 2 * Mathf.PI) % (2 * Mathf.PI) - Mathf.PI, -Time.deltaTime * moveSpeed, Time.deltaTime * moveSpeed);
                touchStartAngle = Mathf.Atan2 (mousePos.y - Screen.height / 2, mousePos.x - Screen.width / 2);
				gonStartAngle = GonAngle;
			}
		}

		for (int i = 0; i < NumberSides; i++) {
			x = mag * Mathf.Cos (angle * i + Mathf.PI / 2.0f + GonAngle);
			y = mag * Mathf.Sin (angle * i + Mathf.PI / 2.0f + GonAngle);
			rotate = ((angle * i + GonAngle) * 180.0f) / Mathf.PI;

			Quaternion rotation = Quaternion.Euler (0, 0, rotate);
			Vector3 pos = new Vector3 (x, y, 0.0f);

			gonSides [i].GetComponent<Transform> ().position = pos;
			gonSides [i].GetComponent<Transform> ().rotation = rotation;
		}
	   }
	
	public void GameOver(){
		GameObject[] GonSides = new GameObject[NumberSides];
		GonSides = GameObject.FindGameObjectsWithTag ("Block");
		GonAngle = 0;
		for (int i = 0; i < NumberSides; i++) {
			Destroy (GonSides[i]);
		}
	}
}
