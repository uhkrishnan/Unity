using UnityEngine;
using GoogleMobileAds.Api;

public class Admob : MonoBehaviour
{
    private string AppID;
    private string BannerAdID;
    public AdPosition BanPosition;
    public bool TestDevice = false;
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
        BannerAdID = "ca-app-pub-3940256099942544/6300978111";
        MobileAds.Initialize(AppID);
        this.CreateBanner(CreateRequest());
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

    #region BannerAd

    public void CreateBanner(AdRequest request)
    {
        this._baneView = new BannerView(BannerAdID, AdSize.SmartBanner, BanPosition);
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

    #endregion

}
