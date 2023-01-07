using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Infrastructure.Services.Save_Load;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Static_Data;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _services;

		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;
			RegisterServices();
		}

		public void Enter()
		{
			_sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
		}

		public void Exit()
		{
			
		}

		private void EnterLoadLevel() => 
			_stateMachine.Enter<LoadProgressState>();

		private void RegisterServices()
		{
			RegisterStaticData();
			_services.RegisterSingle<IInputService>(InputService());
			_services.RegisterSingle<IAssetProvider>(new AssetProvider());
			_services.RegisterSingle<IRandomService>(new RandomService());
			_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
			
			_services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>()));
			_services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
			
			_services.RegisterSingle<IGameFactory>
			(
				new GameFactory(
				_services.Single<IAssetProvider>(),
				_services.Single<IStaticDataService>(),
				_services.Single<IRandomService>(),
				_services.Single<IPersistentProgressService>(),
				_services.Single<IWindowService>())
			);

			_services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
		}

		private void RegisterStaticData()
		{
			IStaticDataService staticData = new StaticDataService();
			staticData.LoadMonsters();
			_services.RegisterSingle(staticData);
		}

		private static IInputService InputService()
		{
			if (Application.isEditor)
				return new StandaloneInputService();
			else
				return new MobileInputService();
		}

	}
}