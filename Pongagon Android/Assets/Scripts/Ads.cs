using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour {
        
    private static InterstitialAd interstitial;

    static public void Init()
    {
        Debug.Log("Ad Loading");
        // Initialize an InterstitialAd.
        InterstitialAd interstitial = new InterstitialAd("ca-app-pub-2709121265681332/6940488201");
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);

    }

    static void Start(){
        Debug.Log("Ad Showing");
        while (!interstitial.IsLoaded());
        interstitial.Show();
    }
    
}
