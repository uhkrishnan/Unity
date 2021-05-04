using UnityEngine;
using GoogleMobileAds.Api;

public class Admob : MonoBehaviour
{
    private string AppID, BannerAdID;
    public static Admob Instance;
    private BannerView _baneView;

    public void Awake()
    {
        if (Instance == null)
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
        AppID = "ca-app-pub-1197472294876964~8464322888";
        //BannerAdID = "ca-app-pub-1197472294876964/9288591026";
        BannerAdID = "ca-app-pub-3940256099942544/6300978111";
        MobileAds.Initialize(AppID);
        this.CreateBanner(CreateRequest());
    }

    private AdRequest CreateRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region BannerAd

    public void CreateBanner(AdRequest request)
    {
        this._baneView = new BannerView(BannerAdID, AdSize.SmartBanner, AdPosition.Bottom);
        this._baneView.LoadAd(request);
        HideBanner();
    }

    public void HideBanner()
    {
        _baneView.Hide();
    }

    public void ShowBanner()
    {
        _baneView.Show();
    }

    public void DestroyBannerAd()
    {
        if (_baneView != null)
            _baneView.Destroy();
    }

    void OnDestroy()
    {
        DestroyBannerAd();
    }

    #endregion
}
