using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

//Banner ad
public class Admob : MonoBehaviour
{
	private BannerView adBanner;
	private UnifiedNativeAd adNative;
	private RewardBasedVideoAd adReward;

	private bool nativeLoaded = false;

	private string idApp, idBanner, idNative, idReward;

	// Native Ad----------------------------
	[SerializeField] GameObject adNativePanel;
	[SerializeField] RawImage adIcon;
	[SerializeField] RawImage adChoices;
	[SerializeField] Text adHeadline;
	[SerializeField] Text adCallToAction;
	[SerializeField] Text adAdvertiser;
	// rewarded Ad----------------------------
	[SerializeField] Button BtnReward; // playAdToContinue button

	[SerializeField] GameObject gameOverPopup;
	[SerializeField] GameObject explosionPrefab;
	[SerializeField] GameObject NoAdsToPlayPopup;

	void Awake()
	{
		adNativePanel.SetActive(false); //hide ad panel
		NoAdsToPlayPopup.SetActive(false);
	}

	void Start()
	{
		//idApp = "ca-app-pub-1197472294876964~8635694762"; // moongabros
		idApp = "ca-app-pub-3413250038717077~5570629877"; //mystique knights
		idBanner = "ca-app-pub-3940256099942544/6300978111";
		idNative = "ca-app-pub-3940256099942544/2247696110";
		idReward = "ca-app-pub-3940256099942544/5224354917";

		this.adReward = RewardBasedVideoAd.Instance;
		MobileAds.Initialize(idApp);

		RequestBannerAd();
		RequestNativeAd();
		RequestRewardAd();
		// Check if device connected to the internet. Fix for adblocker to be added if possible
		StartCoroutine(CheckInternetConnection());
	}

	IEnumerator CheckInternetConnection()
	{
		UnityWebRequest request = new UnityWebRequest("http://google.com");
		yield return request.SendWebRequest();

		if (request.error != null)
		{
			NoAdsToPlayPopup.SetActive(true);
		}
	}

	void Update()
	{
		if (nativeLoaded)
		{
			nativeLoaded = false;

			Texture2D iconTexture = this.adNative.GetIconTexture();
			Texture2D iconAdChoices = this.adNative.GetAdChoicesLogoTexture();
			string headline = this.adNative.GetHeadlineText();
			string cta = this.adNative.GetCallToActionText();
			string advertiser = this.adNative.GetAdvertiserText();
			adIcon.texture = iconTexture;
			adChoices.texture = iconAdChoices;
			adHeadline.text = headline;
			adAdvertiser.text = advertiser;
			adCallToAction.text = cta;

			//register gameobjects
			adNative.RegisterIconImageGameObject(adIcon.gameObject);
			adNative.RegisterAdChoicesLogoGameObject(adChoices.gameObject);
			adNative.RegisterHeadlineTextGameObject(adHeadline.gameObject);
			adNative.RegisterCallToActionGameObject(adCallToAction.gameObject);
			adNative.RegisterAdvertiserTextGameObject(adAdvertiser.gameObject);

			adNativePanel.SetActive(true); //show ad panel
		}
	}

	#region Banner Methods --------------------------------------------------

	public void RequestBannerAd()
	{
		adBanner = new BannerView(idBanner, AdSize.SmartBanner, AdPosition.Bottom);
		AdRequest request = AdRequestBuild();
		adBanner.LoadAd(request);
	}

	public void DestroyBannerAd()
	{
		if (adBanner != null)
			adBanner.Destroy();
	}

	public void HideBanner()
	{
		adBanner.Hide();
	}

	public void ShowBanner()
	{
		adBanner.Show();
	}

	#endregion

	#region Native Ad Mehods ------------------------------------------------

	private void RequestNativeAd()
	{
		AdLoader adLoader = new AdLoader.Builder(idNative).ForUnifiedNativeAd().Build();
		adLoader.OnUnifiedNativeAdLoaded += this.HandleOnUnifiedNativeAdLoaded;
		adLoader.LoadAd(AdRequestBuild());
	}

	//events
	private void HandleOnUnifiedNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
	{
		this.adNative = args.nativeAd;
		nativeLoaded = true;
	}

	#endregion

	#region Reward video methods ---------------------------------------------

	public void RequestRewardAd()
	{
		AdRequest request = AdRequestBuild();
		adReward.LoadAd(request, idReward);

		adReward.OnAdLoaded += this.HandleOnRewardedAdLoaded;

		// Called when an ad request failed to load.
		adReward.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;

		adReward.OnAdRewarded += this.HandleOnAdRewarded;
		adReward.OnAdClosed += this.HandleOnRewardedAdClosed;

	}

	public void ShowRewardAd()
	{
		if (adReward.IsLoaded())
        {
			adReward.Show();
        }
        else
        {
			NoAdsToPlayPopup.SetActive(true);
        }
			
	}
	//events
	public void HandleOnRewardedAdLoaded(object sender, EventArgs args)
	{//ad loaded
		OnGetMorePointsClicked();
		ShowRewardAd();
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		NoAdsToPlayPopup.SetActive(true);
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		NoAdsToPlayPopup.SetActive(true);
	}
	

	public void HandleOnAdRewarded(object sender, EventArgs args)
	{//user finished watching ad

		GameManager.ResumeGame();
		gameOverPopup.SetActive(false);
		explosionPrefab.SetActive(false);
	}


	public void HandleOnRewardedAdClosed(object sender, EventArgs args)
	{//ad closed (even if not finished watching)
		BtnReward.interactable = true;
		BtnReward.GetComponentInChildren<Text>().text = "Play Ads to";

		adReward.OnAdLoaded -= this.HandleOnRewardedAdLoaded;
		adReward.OnAdRewarded -= this.HandleOnAdRewarded;
		adReward.OnAdClosed -= this.HandleOnRewardedAdClosed;
	}

	#endregion

	//------------------------------------------------------------------------

	//other functions
	//btn (more points) clicked
	public void OnGetMorePointsClicked()
	{
		FindObjectOfType<AudioManager>().Play("ClickSound");
		BtnReward.interactable = false;
		BtnReward.GetComponentInChildren<Text>().text = "Loading...";
		//RequestRewardAd();
		ShowRewardAd();
	}

	AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}

	void OnDestroy()
	{
		DestroyBannerAd();
		adReward.OnAdLoaded -= this.HandleOnRewardedAdLoaded;
		adReward.OnAdRewarded -= this.HandleOnAdRewarded;
		adReward.OnAdClosed -= this.HandleOnRewardedAdClosed;
	}

}