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

        public void Construct(IAdsService adsService, IPersistantProgressService progressService)
        {
            base.Construct(progressService);
            _rewardedAdItem.Construct(adsService, progressService);
        }

        protected override void Initialize()
        {
            _rewardedAdItem.Initialize();
            RefreshSkullText();
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
    }
}