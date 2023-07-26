using Infrastructure.AssetManagement;
using Infrastructure.Services.IAP;
using Infrastructure.Services.PersistantProgress;
using Infrastructure.States;
using TMPro;
using UI.Windows.Rewarded;
using UI.Windows.Shop;
using UnityEngine;

namespace UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TMP_Text _skullText;
        [SerializeField] private RewardedAdItem _rewardedAdItem;
        [SerializeField] private ShopItemsContainer _shopItemsContainer;
        

        private IAdsService _adsService;

        private const string INTERSTITAL_PLACEMENT = "DefaultInterstitial";
        
        public void Construct(IAdsService adsService, IPersistantProgressService progressService,
            IGameProcessService gameProcessService, IAssetProvider assetProvider, IIAPService iapService)
        {
            base.Construct(progressService, gameProcessService);
            _rewardedAdItem.Construct(adsService, progressService);
            _adsService = adsService;
            _shopItemsContainer.Construct(iapService, progressService, assetProvider);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _rewardedAdItem.Initialize();
            _shopItemsContainer.Initialize();
            
            RefreshSkullText();
            ShowInterstitialIfAvailable();
        }

        protected override void SubscribeUpdates()
        {
            _rewardedAdItem.Subscribe();
            _shopItemsContainer.Subscribe();
            Progress.CollectedPointsData.OnChanged += RefreshSkullText;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _rewardedAdItem.Cleanup();
            _shopItemsContainer.Cleanup();
            Progress.CollectedPointsData.OnChanged -= RefreshSkullText;
        }

        private void RefreshSkullText() =>
            _skullText.text = Progress.CollectedPointsData.PointsCollected.ToString();

        private void ShowInterstitialIfAvailable() =>
            _adsService.ShowInterstitialIfReady(INTERSTITAL_PLACEMENT);

    }
}