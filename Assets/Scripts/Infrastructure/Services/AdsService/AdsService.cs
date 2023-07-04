using System;
using UnityEngine;

namespace Infrastructure.States
{
    class AdsService : IAdsService
    {
        private AdsBanner _bannerAd;
        private AdsInterstitial _interstitialAd;
        private AdsRewarded _rewardedAd;
        private const string APP_KEY = "1a8f5cbc5";

        public event Action RewardedVideoReady;
        public bool IsRewardedVideoReady => _interstitialAd.IsReady();

        public int Reward { get; } = 13;

        public AdsService()
        {
            CreateAds();

            Initialize();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
            IronSourceEvents.onRewardedVideoAdReadyEvent += RewardedVideoReady;
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

        private void SdkInitializationCompletedEvent()
        {
            _bannerAd.Load();
            _interstitialAd.Load();
            _rewardedAd.Load();
        }

        public void ShowInterstitial() =>
            _interstitialAd.Show();

        public void ShowRewarded() => 
            _rewardedAd.Show();

        void OnApplicationPause(bool isPaused) =>
            IronSource.Agent.onApplicationPause(isPaused);

        public void ShowRewardedVideo(Action onVideoFinished) =>
            ShowRewarded();
    }
}