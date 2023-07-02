using UnityEngine;

public class UnityAdsIronSource : MonoBehaviour
{
    private const string APP_KEY = "1a8f5cbc5";
    
    private void OnEnable()
    {
        Init();
        RegisterEvents();
    }

    private void Start()
    {
        IronSource.Agent.validateIntegration();
    }

    private void OnDisable()
    {
        UnregisterEvents();
    }

    private static void Init() =>
        IronSource.Agent.init(APP_KEY);

    private void RegisterEvents()
    {
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
    }

    private void UnregisterEvents()
    {
        IronSourceBannerEvents.onAdLoadedEvent -= BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent -= BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent -= BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent -= BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent -= BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent -= BannerOnAdLeftApplicationEvent;
        IronSourceEvents.onSdkInitializationCompletedEvent -= SdkInitializationCompletedEvent;
    }

//Invoked once the banner has loaded
    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo) 
    {
    }
//Invoked when the banner loading process has failed.
    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError) 
    {
    }
// Invoked when end user clicks on the banner ad
    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo) 
    {
    }
//Notifies the presentation of a full screen content following user click
    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo) 
    {
    }
//Notifies the presented screen has been dismissed
    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo) 
    {
    }
//Invoked when the user leaves the app
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo) 
    {
    }

    private void SdkInitializationCompletedEvent()
    {
        Debug.Log("Sdk initialized");
        LoadBanner();
    }

    void OnApplicationPause(bool isPaused) =>
        IronSource.Agent.onApplicationPause(isPaused);
    
    public void LoadBanner() =>
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

    public void HideBanner() =>
        IronSource.Agent.hideBanner();

    public void ShowBanner() =>
        IronSource.Agent.displayBanner();

    public void DestroyBanner() =>
        IronSource.Agent.destroyBanner();
}
