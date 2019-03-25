using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
    

	 void Update()
    {
        if (Globals.Music == false ){
            GetComponent<AudioSource>().mute = true;
        }
        else if (Globals.Sound == false){
            GetComponent<AudioSource>().mute = true;
        }
        else if (Globals.Music == true ){
            GetComponent<AudioSource>().mute = false;
        }
        else if (Globals.Sound == true ){
            GetComponent<AudioSource>().mute = false;
            
        }
    
    }
}
