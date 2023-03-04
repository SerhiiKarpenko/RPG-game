using System;
using UnityEngine.Purchasing;

namespace CodeBase.Services.IAP
{
	[Serializable]
	public class ProductConfig
	{
		public string Id;
		public ProductType ProductType;

		public int MaxPurchaseCount;
		public int Quantity;
		public ItemType ItemType;
		public string Price;
		public string Icon;
	}
}