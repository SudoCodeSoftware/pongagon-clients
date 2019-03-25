using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnClick : MonoBehaviour {
    
    public GameObject loadingImage;
    
    
    public void ResetHighscore ()
    {
        PlayerPrefs.SetInt ("High Score", 0);
    }

    public void LoadScene(string Name)
    {
        Application.LoadLevel(Name);
    }
    
    public void Webpage(string Name)
    {
        Application.OpenURL(Name);
    }
    
}
