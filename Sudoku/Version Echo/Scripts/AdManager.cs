using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public string AppID;
    public string BannerAdID;
    public string InterstitialAdID;
    public string RewardedAdID;

    public AdPosition BanPosition;
    public bool TestDevice = false;
    public static AdManager Instance;

    private BannerView _bannerView;
    private InterstitialAd _interstitial;
    private RewardedAd hintRewardedAd;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        MobileAds.Initialize(AppID);
        
        // Version Echo
        /*
        this.CreateBanner(CreateRequest());
        this.CreateInterstitialAd(CreateRequest());
        this.CreateRewardedAd(CreateRequest());
        hintRewardedAd.OnUserEarnedReward += OnUserEarnedHintReward;
        hintRewardedAd.OnAdOpening += OnUserEarnedHintAdOpening;
        */
       
        LoadHint();
    }

    public void LoadHint() // version Echo
    {
        this.CreateBanner(CreateRequest());
        this.CreateInterstitialAd(CreateRequest());
        this.CreateRewardedAd(CreateRequest());
        hintRewardedAd.OnUserEarnedReward += OnUserEarnedHintReward;
        hintRewardedAd.OnAdOpening += OnUserEarnedHintAdOpening;
    }

    /*private void Update()  //version echo
    {

        if (!IsRewardedAdLoaded())
        {
            this.CreateRewardedAd(CreateRequest());
        }
    }*/

    public void OnDestroy()
    {
        hintRewardedAd.OnUserEarnedReward -= OnUserEarnedHintReward;
        hintRewardedAd.OnAdOpening -= OnUserEarnedHintAdOpening;
    }

    private void OnUserEarnedHintAdOpening(object sender, EventArgs e)
    {
        GameEvents.OnGiveAHintAdOpeningMethod();
    }

    private void OnUserEarnedHintReward(object sender, Reward e)
    {
        GameEvents.OnGiveAHintMethod();
    }

    private AdRequest CreateRequest()
    {
        AdRequest request;
        if (TestDevice)
        {
            request = new AdRequest.Builder().AddTestDevice(SystemInfo.deviceUniqueIdentifier).Build();
        }
        else
        {
            request = new AdRequest.Builder().Build();
        }

        return request;

    }

    #region RewardedAd

    public void CreateRewardedAd(AdRequest request)
    {
        this.hintRewardedAd = new RewardedAd(RewardedAdID);
        this.hintRewardedAd.LoadAd(request);
    }

    public void ShowRewardedAd()
    {
        if (this.hintRewardedAd.IsLoaded())
        {
            this.hintRewardedAd.Show();
        }

    }

    public bool IsRewardedAdLoaded()
    {
        return this.hintRewardedAd.IsLoaded();
    }

    #endregion

    #region InterstitialAd

    public void CreateInterstitialAd(AdRequest request)
    {
        this._interstitial = new InterstitialAd(InterstitialAdID);
        this._interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this._interstitial.IsLoaded())
        {
            this._interstitial.Show();
        }

        this._interstitial.LoadAd(CreateRequest());
    }

    #endregion

    #region BannerAd

    public void CreateBanner(AdRequest request)
    {
        this._bannerView = new BannerView(BannerAdID, AdSize.SmartBanner, BanPosition);
        this._bannerView.LoadAd(request);
        HideBanner();
    }

    public void HideBanner()
    {
        _bannerView.Hide();
    }

    public void ShowBanner()
    {
        _bannerView.Show();
    }


    #endregion

}
