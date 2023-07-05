using Common;

namespace Infrastructure.States
{
    public class AdsInterstitial 
    {
        public void Initialize()
        {
            IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
            IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
            IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
            IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
            IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
            IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
            IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
        }

        public void Load() =>
            IronSource.Agent.loadInterstitial();

        public void Show(string placementName) =>
            IronSource.Agent.showInterstitial(placementName);

        public bool IsReady(string placementName) =>
            !IronSource.Agent.isInterstitialPlacementCapped(placementName);

        public void ShowIfReady(string placementName)
        {
            Logger.Log($"Is {placementName} interstital ready {IsReady(placementName)}");
            if (IsReady(placementName)) 
                Show(placementName);
        }

        /************* Interstitial AdInfo Delegates *************/
// Invoked when the interstitial ad was loaded succesfully.
        void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
        {
        }

// Invoked when the initialization process has failed.
        void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
        {
        }

// Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
        void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
        }

// Invoked when end user clicked on the interstitial ad
        void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
        {
        }

// Invoked when the ad failed to show.
        void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
        {
        }

// Invoked when the interstitial ad closed and the user went back to the application screen.
        void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo) =>
            IronSource.Agent.loadInterstitial();

        // Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
// This callback is not supported by all networks, and we recommend using it only if  
// it's supported by all networks you included in your build. 
        void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
        {
        }
    }
}