using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Services.IAP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
	public class ShopItem : MonoBehaviour
	{
		public Button BuyItemButton;
		public TextMeshProUGUI PriceText;
		public TextMeshProUGUI QuantityText;
		public TextMeshProUGUI AvailableItemsLeftText;
		public Image Icon;
		
		private ProductDescription _productDescription;
		private IIAPService _iapService;
		private IAssetProvider _assets;

		public void Construct(IIAPService iapService, IAssetProvider assets, ProductDescription productDescription)
		{
			_iapService = iapService;
			_assets = assets;
			
			_productDescription = productDescription;
		}

		public async void Initialize()
		{
			BuyItemButton.onClick.AddListener(OnBuyItemClick);

			PriceText.text = _productDescription.ProductConfig.Price;
			QuantityText.text = _productDescription.ProductConfig.Quantity.ToString();
			AvailableItemsLeftText.text = _productDescription.AvailablePurchasesLeft.ToString();
			Icon.sprite = await _assets.Load<Sprite>(_productDescription.ProductConfig.Icon);
		}

		private void OnBuyItemClick() => 
			_iapService.StartPurchase(_productDescription.Id);
	}
}