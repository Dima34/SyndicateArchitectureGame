using System;
using Common;

namespace Infrastructure.States
{
    class AdsService : IAdsService
    {
        private AdsBanner _bannerAd;
        private AdsInterstitial _interstitialAd;
        private AdsRewarded _rewardedAd;

        private const string APP_KEY = "1a8f5cbc5";

        public event Action RewardedVideoClosed;

        public AdsService()
        {
            CreateAds();
            Initialize();
            RegisterEvents();
        }

        private void CreateAds()
        {
            _bannerAd = new AdsBanner();
            _interstitialAd = new AdsInterstitial();
            _rewardedAd = new AdsRewarded();
        }

        private void Initialize()
        {
            IronSource.Agent.init(APP_KEY);

            _bannerAd.Initialize();
            _interstitialAd.Initialize();
            _rewardedAd.Initialize();
        }

        private void RegisterEvents()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
            
            _rewardedAd.OnClose += () => RewardedVideoClosed?.Invoke();
        }

        void OnApplicationPause(bool isPaused) =>
            IronSource.Agent.onApplicationPause(isPaused);

        private void SdkInitializationCompletedEvent()
        {
            _bannerAd.Load();
            _interstitialAd.Load();
            _rewardedAd.Load();
        }

        public void ShowInterstitialIfReady(string placementName)
        {
            _interstitialAd.ShowIfReady(placementName);
        }

        public bool IsRewardedVideoReady(string placementName)
        {
            var isRewardedVideoReady = _rewardedAd.IsReady(placementName);
            return isRewardedVideoReady;
        }

        public void ShowRewarded(string placementName) => 
            _rewardedAd.Show(placementName);

        public void ShowRewardedAndExecuteOnEnd(
            string placementName, 
            Action<IronSourcePlacement, IronSourceAdInfo> executeOnEnd)
        {
            ShowRewarded(placementName);
            _rewardedAd.OnRewarded += RecieveReward;
            
            void RecieveReward(IronSourcePlacement placement, IronSourceAdInfo adInfo)
            {
                _rewardedAd.OnRewarded -= RecieveReward;
                executeOnEnd?.Invoke(placement, adInfo);
            }
        }

    }
}