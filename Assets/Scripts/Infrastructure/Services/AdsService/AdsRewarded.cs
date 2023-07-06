using System;
using Logger = Common.Logger;

namespace Infrastructure.States
{
    public class AdsRewarded
    {
        public event Action<IronSourcePlacement, IronSourceAdInfo> OnRewarded;
        public event Action OnReady;
        public event Action OnClose;
        
        public void Initialize()
        {
            IronSourceRewardedVideoEvents.onAdOpenedEvent += OnAdOpened;
            IronSourceRewardedVideoEvents.onAdClosedEvent += OnAdClosed;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += OnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += OnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += OnAdShowFailed;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += AdRewarded;
            IronSourceRewardedVideoEvents.onAdClickedEvent += OnAdClickedEvent;
        }

        public void Load()
        {
            // IronSource.Agent.loadRewardedVideo();
            
        }

        public void Show(string placementName) =>
            IronSource.Agent.showRewardedVideo(placementName);

        public bool IsReady(string placementName) =>
            !IronSource.Agent.isRewardedVideoPlacementCapped(placementName);
        
/************* RewardedVideo AdInfo Delegates *************/
// Indicates that there’s an available ad.
// The adInfo object includes information about the ad that was loaded successfully
// This replaces the RewardedVideoAvailabilityChangedEvent(true) event
        void OnAdAvailable(IronSourceAdInfo adInfo)
        {
            
        }

        // Indicates that no ads are available to be displayed
// This replaces the RewardedVideoAvailabilityChangedEvent(false) event
        void OnAdUnavailable() {
        }
// The Rewarded Video ad view has opened. Your activity will loose focus.
        void OnAdOpened(IronSourceAdInfo adInfo){
        }
// The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
        void OnAdClosed(IronSourceAdInfo adInfo)
        {
            OnClose?.Invoke();
        }

        // The user completed to watch the video, and should be rewarded.
// The placement parameter will include the reward data.
// When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
        void AdRewarded(IronSourcePlacement placement, IronSourceAdInfo adInfo) =>
            OnRewarded?.Invoke(placement, adInfo);

        // The rewarded video ad was failed to show.
        void OnAdShowFailed(IronSourceError error, IronSourceAdInfo adInfo){
        }
// Invoked when the video ad was clicked.
// This callback is not supported by all networks, and we recommend using it only if
// it’s supported by all networks you included in your build.
        void OnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo){
        }

    }
}