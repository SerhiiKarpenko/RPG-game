using CodeBase.Infrastructure.Services;
using System;

namespace CodeBase.Services.Ads
{
	public interface IAdsService : IService
	{
		event Action RewardedVideoReady;
		int Reward { get; }
		bool IsRewardedVideoReady { get; }
		void Initialize();
		void ShowRewardedVideo(Action onVideoFinished);
	}
}