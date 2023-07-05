using Infrastructure.Services.PersistantProgress;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Rewarded
{
    public class RewardedAdItem : MonoBehaviour
    {
        [SerializeField] private Button _showAdButton;
        [SerializeField] private GameObject[] _adActiveObjects;
        [SerializeField] private GameObject[] _adInActiveObjects;
        
        private IAdsService _adsService;
        private IPersistantProgressService _progressService;
        
        private const string REWARDED_VIDEO_PLACEMENT = "Startup";

        public void Construct(IAdsService adsService, IPersistantProgressService progressService)
        {
            _progressService = progressService;
            _adsService = adsService;
        }
        
        public void Initialize()
        {
            _showAdButton.onClick.AddListener(OnShowAdClicked);

            RefreshAvailableAd();
        }

        public void Subscribe()
        {
            _adsService.RewardedVideoReady += RefreshAvailableAd;
            _adsService.RewardedVideoClosed += RefreshAvailableAd;
        }

        public void Cleanup()
        {
            _adsService.RewardedVideoReady -= RefreshAvailableAd;
            _adsService.RewardedVideoClosed -= RefreshAvailableAd;
        }

        private void OnShowAdClicked() =>
            _adsService.ShowRewardedAndExecuteOnEnd(REWARDED_VIDEO_PLACEMENT, OnVideoEnded);
        
        private void OnVideoEnded(IronSourcePlacement placement, IronSourceAdInfo adInfo) =>
            _progressService.Progress.CollectedPointsData.Add(placement.getRewardAmount());

        private void RefreshAvailableAd() 
        {
            var videoReady = _adsService.IsRewardedVideoReady(REWARDED_VIDEO_PLACEMENT);

            foreach (GameObject activeObject in _adActiveObjects) 
                activeObject.SetActive(videoReady);
            
            foreach (GameObject inActiveObject in _adInActiveObjects) 
                inActiveObject.SetActive(videoReady);
        }
        
        
    }
}