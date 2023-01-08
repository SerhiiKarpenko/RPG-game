using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Services.Ads
{
	public class AdsService : IAdsService, IUnityAdsListener
	{
		public event Action RewardedVideoReady;
		public int Reward => 13;

		private const string AndroidGameId = "5109991";
		private const string IOSGameId = "5109990";
		private const string RewardedVideoPlacementId = "rewardedVideo";
		
		private Action _onVideoFinished;
		private string _gameId;


		public void Initialize()
		{
			switch (Application.platform)
			{
				case RuntimePlatform.Android:
					_gameId = AndroidGameId;
					break;
				case RuntimePlatform.IPhonePlayer:
					_gameId = IOSGameId;
					break;
				case RuntimePlatform.WindowsEditor:
					_gameId = AndroidGameId;
					break;
				default:
					Debug.Log("Unsupported platform for ads");
					break;
			}
			
			Advertisement.AddListener(this);
			Advertisement.Initialize(_gameId);
		}

		public bool IsRewardedVideoReady =>
			Advertisement.IsReady(RewardedVideoPlacementId);

		public void ShowRewardedVideo(Action onVideoFinished)
		{
			Advertisement.Show(RewardedVideoPlacementId);
			
			_onVideoFinished = onVideoFinished;
		}

		public void OnUnityAdsReady(string placementId)
		{
			Debug.Log($"OnUnityAdsReady {placementId}");
			if (placementId == RewardedVideoPlacementId)
				RewardedVideoReady?.Invoke();
		}

		public void OnUnityAdsDidError(string message) => 
			Debug.Log($"OnUnityAdsDidError {message}");

		public void OnUnityAdsDidStart(string placementId) => 
			Debug.Log($"OnUnityAdsDidStart {placementId}");

		public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
		{
			switch (showResult)
			{
				case ShowResult.Failed:
					Debug.LogError($"OnUnityAdsDidFinish {showResult}");
					break;
				case ShowResult.Skipped:
					Debug.LogError($"OnUnityAdsDidFinish {showResult}");
					break;
				case ShowResult.Finished:
					_onVideoFinished?.Invoke();
					break;
				default:
					Debug.LogError($"OnUnityAdsDidFinish {showResult}");
					break;
			}

			_onVideoFinished = null;
		}

		public void Dispose() { }
	}
}
