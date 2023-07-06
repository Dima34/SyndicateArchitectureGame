using Infrastructure.Services.PersistantProgress;
using Infrastructure.States;
using TMPro;
using UI.Windows.Rewarded;
using UnityEngine;

namespace UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TMP_Text _skullText;
        [SerializeField] private RewardedAdItem _rewardedAdItem;

        private IAdsService _adsService;

        private const string INTERSTITAL_PLACEMENT = "DefaultInterstitial";
        
        public void Construct(IAdsService adsService, IPersistantProgressService progressService,
            IGameProcessService gameProcessService)
        {
            base.Construct(progressService, gameProcessService);
            _rewardedAdItem.Construct(adsService, progressService);
            _adsService = adsService;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _rewardedAdItem.Initialize();
            RefreshSkullText();
            ShowInterstitialIfAvailable();
        }

        protected override void SubscribeUpdates()
        {
            _rewardedAdItem.Subscribe();
            Progress.CollectedPointsData.OnChanged += RefreshSkullText;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _rewardedAdItem.Cleanup();
            Progress.CollectedPointsData.OnChanged -= RefreshSkullText;
        }

        private void RefreshSkullText() =>
            _skullText.text = Progress.CollectedPointsData.PointsCollected.ToString();

        private void ShowInterstitialIfAvailable() =>
            _adsService.ShowInterstitialIfReady(INTERSTITAL_PLACEMENT);

    }
}