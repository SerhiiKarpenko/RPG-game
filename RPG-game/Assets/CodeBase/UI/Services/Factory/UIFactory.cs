using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Static_Data.Windows;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Shop;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
	public class UIFactory : IUIFactory
	{
		private const string UIRootPath = "UIRoot";
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private Transform _uiRoot;
		private readonly IPersistentProgressService _progressService;
		private readonly IAdsService _adsService;

		public UIFactory(IAssetProvider assets, IStaticDataService staticData,
			IPersistentProgressService progressService, IAdsService adsService)
		{
			_assets = assets;
			_staticData = staticData;
			_progressService = progressService;
			_adsService = adsService;
		}

		public void CreateShop()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.Shop);
			ShopWindow window = Object.Instantiate(config.prefab, _uiRoot) as ShopWindow;
			window.Construct(_adsService, _progressService);
		}
		
		public async Task CreateUIRoot()
		{
			GameObject root = await _assets.Instantiate(UIRootPath);
			_uiRoot = root.transform;
		}

		public void Dispose() { }
	}
}