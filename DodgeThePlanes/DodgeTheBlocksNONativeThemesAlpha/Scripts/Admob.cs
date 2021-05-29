using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class Admob : MonoBehaviour
{
	private BannerView adBanner;
	private RewardedAd rewardedAd;
	private string idBanner, idReward;
	
	[SerializeField] GameObject gameOverPopup;
	[SerializeField] GameObject explosionPrefab;
	[SerializeField] GameObject NoAdsToPlayPopup;
	[SerializeField] Button playAdButton;
	[SerializeField] Button restartButton;
	[SerializeField] PolygonCollider2D playerSkinCollider;
	[SerializeField] GameObject playerAnimator;
	[SerializeField] GameObject playerSkinParent;

	private Animator playerAnimatorComponent;
	private Text playAdButtonComponent;
	private Text restartButtonComponent;

	void Awake()
	{
		NoAdsToPlayPopup.SetActive(false);
	}

	void Start()
	{
		idBanner = "ca-app-pub-3940256099942544/6300978111";
		idReward = "ca-app-pub-3940256099942544/5224354917";
		MobileAds.Initialize(initStatus => { });
		RequestBannerAd();
		CreateAndLoadRewardedAd();
		playerAnimatorComponent = playerAnimator.GetComponent<Animator>();
		playAdButtonComponent = playAdButton.GetComponentInChildren<Text>();
		restartButtonComponent = restartButton.GetComponentInChildren<Text>();

		//playerAnimator.GetComponent<Animator>().enabled = false;
		playerAnimatorComponent.enabled = false;
		playerSkinCollider.enabled = true;
		playerSkinParent.SetActive(true);
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

	#region Reward Ad methods ---------------------------------------------
	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		NoAdsToPlayPopup.SetActive(false);
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	{
		NoAdsToPlayPopup.SetActive(true);
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		NoAdsToPlayPopup.SetActive(true);
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{

		NoAdsToPlayPopup.SetActive(true);
		playerSkinCollider.enabled = false;
		playerAnimator.GetComponent<Animator>().enabled = true;
		this.Wait(3.1f, () =>
		{
			DisableGhostMode();
		}
		);
		
	}

    public void HandleUserEarnedReward(object sender, Reward args)
	{
		playAdButton.interactable = true;
		//playAdButton.GetComponentInChildren<Text>().text = "PLAY AD TO";
		playAdButtonComponent.text = "PLAY AD TO";
		//restartButton.GetComponentInChildren<Text>().text = "";
		restartButtonComponent.text = "";
		NoAdsToPlayPopup.SetActive(true);
		gameOverPopup.SetActive(false);
		explosionPrefab.SetActive(false);
		GameManager.ResumeGame();
	}

	private void DisableGhostMode()
    {
		playerSkinParent.SetActive(true);
		playerSkinCollider.enabled = true;
		//playerAnimator.GetComponent<Animator>().enabled = false;
		playerAnimatorComponent.enabled = false;
	}

	private void UserChoseToWatchAd()
	{
		if (this.rewardedAd.IsLoaded())
		{
			this.rewardedAd.Show();
		}
	}

	public void CreateAndLoadRewardedAd()
	{
		this.rewardedAd = new RewardedAd(idReward);
		this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
		this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
		this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
		this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
		AdRequest request = AdRequestBuild();
		this.rewardedAd.LoadAd(request);
	}
	#endregion

	public void OnGetMorePointsClicked()
	{
		playAdButton.interactable = false;
		//playAdButton.GetComponentInChildren<Text>().text = "Loading...";
		playAdButtonComponent.text = "Loading...";
		UserChoseToWatchAd();
	}

	AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}

	void OnDestroy()
	{
		DestroyBannerAd();
	}
}