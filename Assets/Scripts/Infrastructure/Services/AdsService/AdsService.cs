using System;

namespace Infrastructure.States
{
    class AdsService : IAdsService
    {
        public event Action RewardedVideoReady;
        public bool IsRewardedVideoReady { get; set; }
        public int Reward { get; } = 13;
        public void ShowRewardedVideo(Action onVideoFinished)
        {
            throw new NotImplementedException();
        }
    }
}

