using System;
using CodeBase.Services.Interface;
using Cysharp.Threading.Tasks;

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