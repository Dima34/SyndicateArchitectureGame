using System;
using Infrastructure.Services;

namespace Infrastructure.States
{
    public interface IAdsService : IService
    {
        event Action RewardedVideoClosed;
        bool IsRewardedVideoReady(string placementName);
        void ShowInterstitialIfReady(string placementName);
        void ShowRewardedAndExecuteOnEnd(string placementName,
            Action<IronSourcePlacement, IronSourceAdInfo> executeOnEnd);

    }
}