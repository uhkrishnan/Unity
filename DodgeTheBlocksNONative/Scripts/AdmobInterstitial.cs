﻿using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmobInterstitial : MonoBehaviour
{
	private InterstitialAd interstitial;
	private string idInterstitial;

	void Start()
	{
		idInterstitial = "ca-app-pub-3940256099942544/1033173712";
		MobileAds.Initialize(initStatus => { });
		RequestInterstitialAd();
	}

	#region Interstitial

	public void RequestInterstitialAd()
	{
		this.interstitial = new InterstitialAd(idInterstitial);
		AdRequest request = new AdRequest.Builder().Build();
		this.interstitial.LoadAd(request);
		this.interstitial.OnAdClosed += HandleOnAdClosed;
	}

	public void PlayGameAfterAd()
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
        }
        else
        {
			SceneController.LoadNextScene();
		}
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		SceneController.LoadNextScene();
		interstitial.OnAdClosed -= this.HandleOnAdClosed;
		RequestInterstitialAd();
	}

	#endregion

	void OnDestroy()
	{
		interstitial.Destroy();
	}

}