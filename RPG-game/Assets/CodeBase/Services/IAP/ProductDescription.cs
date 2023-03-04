using UnityEngine.Purchasing;

namespace CodeBase.Services.IAP
{
	public class ProductDescription
	{
		public string Id;
		public Product Product;
		public ProductConfig ProductConfig;
		public int AvailablePurchasesLeft;
	}
}