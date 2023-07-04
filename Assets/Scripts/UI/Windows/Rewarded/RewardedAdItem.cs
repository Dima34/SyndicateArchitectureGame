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

        public void Subscribe() =>
            _adsService.RewardedVideoReady += RefreshAvailableAd;

        public void Cleanup() =>
            _adsService.RewardedVideoReady -= RefreshAvailableAd;

        private void OnShowAdClicked() =>
            _adsService.ShowRewardedVideo(OnVideoFinished);

        private void OnVideoFinished() =>
            _progressService.Progress.CollectedPointsData.Add(_adsService.Reward);

        private void RefreshAvailableAd()
        {
            var videoReady = _adsService.IsRewardedVideoReady;

            foreach (GameObject activeObject in _adActiveObjects) 
                activeObject.SetActive(videoReady);
            
            foreach (GameObject inActiveObject in _adInActiveObjects) 
                inActiveObject.SetActive(videoReady);
        }

        public void GetRewardCount(string placementName)
        {
            IronSource.Agent.getPlacementInfo(placementName);
        }
    }
}