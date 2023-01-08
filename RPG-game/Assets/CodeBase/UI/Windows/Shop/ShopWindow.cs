using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Services.Ads;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
	public class ShopWindow : WindowBase
	{
		[SerializeField] private TextMeshProUGUI _skullText;
		[SerializeField] private RewardedAdItem _adItem;

		public void Construct(IAdsService adsService, IPersistentProgressService progressService)
		{
			base.Construct(progressService);
			_adItem.Construct(adsService, progressService);
		}
		
		protected override void Initialize()
		{
			_adItem.Initialize();
			RefreshSkullText();
		}

		protected override void SubscribeUpdates()
		{
			_adItem.Subscribe();
			Progress.WorldData.LootData.Changed += RefreshSkullText;
		}

		protected override void Cleanup()
		{
			base.Cleanup();
			_adItem.Cleanup();
			Progress.WorldData.LootData.Changed -= RefreshSkullText;
		}

		private void RefreshSkullText() => 
			_skullText.text = Progress.WorldData.LootData.Collected.ToString();
	}
}