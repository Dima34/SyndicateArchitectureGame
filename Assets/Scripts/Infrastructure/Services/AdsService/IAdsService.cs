using System;
using Infrastructure.Services;

namespace Infrastructure.States
{
    public interface IAdsService : IService
    {
        event Action RewardedVideoReady;
        bool IsRewardedVideoReady { get; set; }
        int Reward { get; }
        void ShowRewardedVideo(Action onVideoFinished);
    }
}