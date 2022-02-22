using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsManager1 : MonoBehaviour {
	
	public static AdsManager1 instance;

	private BannerView banner;
	private string bannerID = "ca-app-pub-3940256099942544/6300978111";

	private InterstitialAd interstitial;
	private string interstitialID = "ca-app-pub-3940256099942544/1033173712";

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}
	}
		
	void Start () {
		MobileAds.Initialize(initStatus => { });
		RequestInterstitial ();
	}

	public void RequestBanner(){
		banner = new BannerView (bannerID, AdSize.SmartBanner, AdPosition.Bottom);
		AdRequest request = new AdRequest.Builder ().Build ();
		banner.LoadAd (request);
		banner.Show ();
	}

	public void HideBanner(){
		banner.Hide ();
	}

	public void RequestInterstitial(){
		interstitial = new InterstitialAd (interstitialID);
		AdRequest request = new AdRequest.Builder ().Build ();
		interstitial.LoadAd (request);
	}

	public void ShowInterstitial(){
		if (interstitial.IsLoaded ()) {
			interstitial.Show ();
		} else {
			Debug.Log ("Interstitial Ad Not Loaded!");
		}
	}

}
