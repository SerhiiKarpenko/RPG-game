using System;
using System.Collections.Generic;
using CodeBase.Services.Interface;

namespace CodeBase.Services.IAP
{
	public interface IIAPService : IService
	{
		bool IsInitialized { get; }
		event Action Initialized;
		void Initialize();
		List<ProductDescription> Products();
		void StartPurchase(string productId);
	}
}