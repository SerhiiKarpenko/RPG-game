using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Services.IAP;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Services.Persistent_Progress;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
	public class ShopItemContainer : MonoBehaviour
	{
		private const string ShopItemPath = "Shop Item";
		
		public GameObject[] ShopUnavailableObjects;
		public Transform ParentLayout;
		
		private IIAPService _iapService;
		private IPersistentProgressService _progressService;
		private IAssetProvider _assetProvider;
		
		private readonly List<GameObject> _shopItems = new List<GameObject>();

		public void Construct(IIAPService iapService, IPersistentProgressService progressService,
			IAssetProvider assetProvider)
		{
			_iapService = iapService;
			_progressService = progressService;
			_assetProvider = assetProvider;
		}

		public void Initialize() => 
			RefreshAvailableItems();

		public void Subscribe()
		{
			_iapService.Initialized += RefreshAvailableItems;
			_progressService.Progress.PurchaseData.Changed += RefreshAvailableItems;
		}

		public void CleanUp()
		{
			_iapService.Initialized -= RefreshAvailableItems;
			_progressService.Progress.PurchaseData.Changed -= RefreshAvailableItems;
		}

		private async void RefreshAvailableItems()
		{
			UpdateUnavailableObjects();

			if (!_iapService.IsInitialized)
				return;

			ClearShopItems();

			await FillShopItems();
		}

		private void ClearShopItems()
		{
			foreach (GameObject shopItem in _shopItems)
				Destroy(shopItem);
		}

		private async Task FillShopItems()
		{
			foreach (ProductDescription productDescription in _iapService.Products())
			{
				GameObject shopItemObject = await _assetProvider.Instantiate(ShopItemPath, ParentLayout);
				ShopItem shopItem = shopItemObject.GetComponent<ShopItem>();

				shopItem.Construct(_iapService, _assetProvider, productDescription);
				shopItem.Initialize();

				_shopItems.Add(shopItemObject);
			}
		}

		private void UpdateUnavailableObjects()
		{
			foreach (GameObject shopUnavailableObject in ShopUnavailableObjects)
				shopUnavailableObject.SetActive(!_iapService.IsInitialized);
		}
	}
}