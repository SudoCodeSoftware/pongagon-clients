using UnityEngine;
using System.Collections;
using ADInterstitialAd = UnityEngine.iOS.ADInterstitialAd;



public class Ads : MonoBehaviour {
        
    private static ADInterstitialAd fullscreenAd = null;
    private static bool Shown = false;


    static public void Init()
    {
        Debug.Log("Loading Ad");
        Shown = false;
        fullscreenAd = new ADInterstitialAd(true);
        ADInterstitialAd.onInterstitialWasLoaded  += OnFullscreenLoaded;
    }

    static void OnFullscreenLoaded() {
        Debug.Log("Ad Loaded");
    }

    static void Start()
    {
            if (!Shown){
                Debug.Log("Displaying Ad");
                fullscreenAd.Show();
                Shown = true;
            }

    }
}
