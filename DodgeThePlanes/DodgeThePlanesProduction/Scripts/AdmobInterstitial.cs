using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmobInterstitial : MonoBehaviour
{
	private InterstitialAd interstitial;
	private string idInterstitial;

	void Start()
	{
		idInterstitial = "ca-app-pub-1197472294876964/8742713381";
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
			int theme = GameDataManager.SelectedTheme();
			if(theme == 0)
            {
				SceneController.LoadScene(2);
            }
            else
            {
				SceneController.LoadScene(3);
			}
		}
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		int theme = GameDataManager.SelectedTheme();
		if (theme == 0)
		{
			SceneController.LoadScene(2);
		}
		else
		{
			SceneController.LoadScene(3);
		}
		interstitial.OnAdClosed -= this.HandleOnAdClosed;
		RequestInterstitialAd();
	}

	#endregion

	void OnDestroy()
	{
		interstitial.Destroy();
	}

}