using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Services.Ads;
using CodeBase.Services.IAP;
using CodeBase.Services.Persistent_Progress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
	public class ShopWindow : WindowBase
	{
		[SerializeField] private TextMeshProUGUI _skullText;
		[SerializeField] private RewardedAdItem _adItem;
		[SerializeField] private ShopItemContainer _shopItemsContainer;

		public void Construct(
			IAdsService adsService,
			IPersistentProgressService progressService,
			IIAPService iapService,
			IAssetProvider assetProvider)
		{
			base.Construct(progressService);
			_adItem.Construct(adsService, progressService);
			_shopItemsContainer.Construct(iapService, progressService, assetProvider);
		}
		
		protected override void Initialize()
		{
			_adItem.Initialize();
			_shopItemsContainer.Initialize();
			RefreshSkullText();
		}

		protected override void SubscribeUpdates()
		{
			_adItem.Subscribe();
			_shopItemsContainer.Subscribe();
			Progress.WorldData.LootData.Changed += RefreshSkullText;
		}

		protected override void Cleanup()
		{
			base.Cleanup();
			_adItem.Cleanup();
			_shopItemsContainer.CleanUp();
			Progress.WorldData.LootData.Changed -= RefreshSkullText;
		}

		private void RefreshSkullText() => 
			_skullText.text = Progress.WorldData.LootData.Collected.ToString();
	}
}