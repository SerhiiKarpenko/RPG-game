using CodeBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.Persistent_Progress;
using UnityEngine.Purchasing;

namespace CodeBase.Services.IAP
{
	public class IAPService : IIAPService
	{
		public bool IsInitialized => _iapProvider.IsInitialized;
		public event Action Initialized;
		
		private readonly IAPProvider _iapProvider;
		private readonly IPersistentProgressService _progressService;

		public IAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
		{
			_iapProvider = iapProvider;
			_progressService = progressService;
		}

		public void Initialize()
		{
			_iapProvider.Initialize(this);
			_iapProvider.Initialized += () => Initialized?.Invoke();
		}

		public List<ProductDescription> Products() =>
			ProductDescriptions().ToList();

		public void StartPurchase(string productId) =>
			_iapProvider.StartPurchase(productId);

		public PurchaseProcessingResult ProcessPurchase(Product purchasedProduct)
		{
			ProductConfig productConfig = _iapProvider.Configs[purchasedProduct.definition.id];

			switch (productConfig.ItemType)
			{
				case ItemType.Skulls:
					_progressService.Progress.WorldData.LootData.Add(productConfig.Quantity);
					_progressService.Progress.PurchaseData.AddPurchase(purchasedProduct.definition.id);
					break;
			}

			return PurchaseProcessingResult.Complete;
		}

		private bool ProductBoughtOut(BoughtIAP boughtIAP, ProductConfig config) => 
			boughtIAP != null && boughtIAP.IACount >= config.MaxPurchaseCount;

		private IEnumerable<ProductDescription> ProductDescriptions()
		{
			PurchaseData purchaseData = _progressService.Progress.PurchaseData;
			
			foreach (string productId in _iapProvider.Products.Keys)
			{
				ProductConfig config = _iapProvider.Configs[productId];
				Product product = _iapProvider.Products[productId];

				BoughtIAP boughtIAP = purchaseData.BoughtIAPs.Find(x => x.IAPid == productId);

				if (ProductBoughtOut(boughtIAP, config))
					continue;

				yield return new ProductDescription
				{
					Id = productId,
					ProductConfig = config,
					Product = product,
					AvailablePurchasesLeft = boughtIAP != null
						? config.MaxPurchaseCount - boughtIAP.IACount
						: config.MaxPurchaseCount,
				};
			} 
		}

		public void Dispose() { }
	}
}