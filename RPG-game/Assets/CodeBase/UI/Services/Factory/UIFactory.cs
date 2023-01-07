using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Services;
using CodeBase.Static_Data.Windows;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
	public class UIFactory : IUIFactory
	{
		private const string UIRootPath = "UI/UI Root";
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private Transform _uiRoot;
		private readonly IPersistentProgressService _progressService;

		public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService)
		{
			_assets = assets;
			_staticData = staticData;
			_progressService = progressService;
		}

		public void CreateShop()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.Shop);
			WindowBase window = Object.Instantiate(config.prefab, _uiRoot);
			window.Construct(_progressService);
		}
		
		public void CreateUIRoot() => 
			_uiRoot = _assets.Instantiate(UIRootPath).transform;

		public void Dispose() { }
	}
}