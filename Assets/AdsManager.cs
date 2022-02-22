using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour {

	public Text rewardText;

	private BannerView bannerView;
	private InterstitialAd interstitial;
	private RewardedAd rewardedAd;


	void Start () {
		MobileAds.Initialize(initStatus => { });

//		this.RequestBanner();
//		this.RequestInterstitial ();
//		this.RequestRewarded ();
	}

	public void RequestRewarded(){
		string adUnitId;
		#if UNITY_ANDROID
		adUnitId = "ca-app-pub-3940256099942544/5224354917";
		#elif UNITY_IPHONE
		adUnitId = "unexpected_platform";
		#endif

		this.rewardedAd = new RewardedAd(adUnitId);

		// Called when an ad request has successfully loaded.
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		// Called when an ad is shown.
		this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);
	}

	public void ShowRewardedAd(){
		if (this.rewardedAd.IsLoaded()) {
			this.rewardedAd.Show();
			this.RequestRewarded ();
		}
	}
	
	private void RequestBanner() {
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the top of the screen.
		this.bannerView = new BannerView (adUnitId, AdSize.Banner, AdPosition.Top);

		// Called when an ad request has successfully loaded.
		this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
		// Called when an ad is clicked.
		this.bannerView.OnAdOpening += this.HandleOnAdOpened;
		// Called when the user returned from the app after an ad click.
		this.bannerView.OnAdClosed += this.HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		this.bannerView.LoadAd(request);
	}

	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/1033173712";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd (adUnitId);

		// Called when an ad request has successfully loaded.
		this.interstitial.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		this.interstitial.OnAdOpening += HandleOnAdOpened;
		// Called when the ad is closed.
		this.interstitial.OnAdClosed += HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);
	}

	public void ShowInterstitial()
	{
		if (this.interstitial.IsLoaded()) {
			this.interstitial.Show();
			this.RequestInterstitial ();
		}
	}


	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
		rewardText.text = "reward ad loaded";
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToLoad event received with message: "
			+ args.Message);
		rewardText.text = "reward ad failed to load";
	}

	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
		rewardText.text = "reward ad openning";
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
			+ args.Message);
		rewardText.text = "reward ad failed to show";
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
		rewardText.text = "reward ad closed!";
	}

	public void HandleUserEarnedReward(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		MonoBehaviour.print(
			"HandleRewardedAdRewarded event received for "
			+ amount.ToString() + " " + type);
//		rewardText.text = "HandleRewardedAdRewarded event received for " + amount.ToString () + " " + type;
		rewardText.text = "reward earned";
	}


	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
			+ args.Message);
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeavingApplication event received");
	}

}
