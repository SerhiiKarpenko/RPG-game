using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Infrastructure.Services;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.IAP;

namespace CodeBase.Infrastructure
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly IAssetProvider _assetProvider;
		private readonly IAdsService _adsService;
		private readonly IIAPService _iapService;
		private readonly IStaticDataService _staticDataService;
		private readonly AllServices _services;

		public BootstrapState(
			GameStateMachine stateMachine,
			SceneLoader sceneLoader,
			IAssetProvider assetProvider,
			IAdsService adsService,
			IIAPService iapService,
			IStaticDataService staticDataService
			)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_assetProvider = assetProvider;
			_adsService = adsService;
			_iapService = iapService;
			_staticDataService = staticDataService;
		}

		public async void Enter()
		{
			// we can always make this initialize methods async, to await them all full initialize
			await _assetProvider.Initialize();
			
			_staticDataService.LoadMonsters();
			_iapService.Initialize();
			_adsService.Initialize();
			
			_sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
		}

		public void Exit()
		{
			
		}

		private void EnterLoadLevel() => 
			_stateMachine.Enter<LoadProgressState>();
	}
}