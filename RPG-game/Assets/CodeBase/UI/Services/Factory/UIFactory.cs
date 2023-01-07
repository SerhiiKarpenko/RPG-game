using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Services;
using CodeBase.Static_Data.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
	public class UIFactory : IUIFactory
	{
		private const string UIRootPath = "UI/UI Root";
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private Transform _uiRoot;
		
		public UIFactory(IAssetProvider assets, IStaticDataService staticData)
		{
			_assets = assets;
			_staticData = staticData;
		}

		public void CreateShop()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.Shop);
			Object.Instantiate(config.prefab, _uiRoot);
		}
		
		public void CreateUIRoot() => 
			_uiRoot = _assets.Instantiate(UIRootPath).transform;

		public void Dispose() { }
	}
}