using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sound : MonoBehaviour {

    public Toggle All;
    public Toggle Music;    
    
	 void Start() 
    {
        Music.isOn = Globals.Music;
        All.isOn = Globals.Sound;
    }
    
    
    public void MuteAll ()
    {
        Globals.Sound = All.isOn;
        Globals.Music = All.isOn;
        Music.isOn = Globals.Sound;
        Music.interactable = Globals.Sound;
    }
    public void MuteMusic()
    {
        Globals.Music = Music.isOn;
        }
    
    void Update()
    {
        if (GetComponent<AudioSource>()){
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
}
