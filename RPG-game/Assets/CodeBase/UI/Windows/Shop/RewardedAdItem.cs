using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Services.Ads;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
	public class RewardedAdItem : MonoBehaviour
	{
		[SerializeField] private Button _showAdButton;
		[SerializeField] private GameObject[] _addActiveObjects;
		[SerializeField] private GameObject[] _addInactiveObjects;
		private IAdsService _adsService;
		private IPersistentProgressService _progressService;

		public void Construct(IAdsService adsService, IPersistentProgressService progressService)
		{
			_adsService = adsService;
			_progressService = progressService;
		}
		
		public void Initialize()
		{
			_showAdButton.onClick.AddListener(OnShowAddClicked);
			RefreshAvailableAd();
		}

		public void Subscribe() => 
			_adsService.RewardedVideoReady += RefreshAvailableAd;

		public void Cleanup() => 
			_adsService.RewardedVideoReady -= RefreshAvailableAd;

		private void OnShowAddClicked() => 
			_adsService.ShowRewardedVideo(OnVideoFinished);

		private void OnVideoFinished() => 
			_progressService.Progress.WorldData.LootData.Add(_adsService.Reward);

		private void RefreshAvailableAd()
		{
			bool videoReady = _adsService.IsRewardedVideoReady;
			
			foreach (GameObject addActiveObject in _addActiveObjects)
				addActiveObject.SetActive(videoReady);
			
			foreach (GameObject addInactiveObject in _addInactiveObjects)
				addInactiveObject.SetActive(!videoReady);
		}
	}
}