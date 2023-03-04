using CodeBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

namespace CodeBase.Services.IAP
{
	public class IAPProvider : IStoreListener
	{
		private const string IAPConfigsPath = "IAP/products";
		
		public Dictionary<string ,ProductConfig> Configs { get; private set; }
		public Dictionary<string ,Product> Products { get; private set; }
		
		public event Action Initialized;
		
		private IStoreController _controller;
		private IExtensionProvider _extensions;
		private IAPService _iapService;
		
		public bool IsInitialized => _controller != null && _extensions != null;

		public void Initialize(IAPService iapService)
		{
			_iapService = iapService;
			Configs = new Dictionary<string, ProductConfig>();
			Products = new Dictionary<string, Product>();
			
			Load();
			
			ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			
			foreach (ProductConfig productConfig in Configs.Values)
				builder.AddProduct(productConfig.Id, productConfig.ProductType);
			
			UnityPurchasing.Initialize(this, builder);
		}

		public void StartPurchase(string productId) => 
			_controller.InitiatePurchase(productId);

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			_controller = controller;
			_extensions = extensions;

			foreach (Product product in controller.products.all)
				Products.Add(product.definition.id, product);
			
			Initialized?.Invoke();
			
			Debug.Log("UnityPurchasing initialization success");
		}


		public void OnInitializeFailed(InitializationFailureReason error) => 
			Debug.Log($"UnityPurchasing initialization OnInitializeFailed: {error}");

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
		{
			Debug.Log($"ProcessPurchase success {purchaseEvent.purchasedProduct.definition.id}");
			
			return _iapService.ProcessPurchase(purchaseEvent.purchasedProduct);
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => 
			Debug.LogError($"Product {product.definition.id} purchase failed, PurchaseFailure {failureReason}, transaction id {product.transactionID} ");

		private void Load() => 
			Configs = Resources.
				Load<TextAsset>(IAPConfigsPath)
				.text.
				ToDeserialized<ProductConfigWrapper>().
				Configs.
				ToDictionary(x => x.Id, x =>x);
	}
}