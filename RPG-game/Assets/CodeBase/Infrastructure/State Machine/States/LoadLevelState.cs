using CodeBase.Camera_Logic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Services;
using CodeBase.Static_Data;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";
		private const string EnemySpawnerTag = "EnemySpawner";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _progressService;
		private readonly IStaticDataService _staticData;
		private readonly IUIFactory _uiFactory;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
			IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData,
			IUIFactory uiFactory)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
			_staticData = staticData;
			_uiFactory = uiFactory;
		}

		public async void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_gameFactory.CleanUp();
			await _gameFactory.WarmUp();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() => 
			_loadingCurtain.Hide();

		private async void OnLoaded()
		{
			await InitUIRoot();
			await InitGameWorld();
			InformProgressReaders();
			
			_stateMachine.Enter<GameLoopState>();
		}

		private async Task InitUIRoot() => 
			await _uiFactory.CreateUIRoot();

		private void InformProgressReaders()
		{
			foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
			{
				progressReader.LoadProgress(_progressService.Progress);
			}
		}

		private async Task InitGameWorld()
		{
			LevelStaticData levelData = LevelStaticData();

			await InitSpawners(levelData);
			GameObject hero = await InitHero(levelData);
			await InitHud(hero);
			
			CameraFollow(hero);
		}

		private async Task InitSpawners(LevelStaticData levelData)
		{
			foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
				await _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeId);
		}

		private async Task<GameObject> InitHero(LevelStaticData levelStaticData) => 
			await _gameFactory.CreateHero(levelStaticData.InitialHeroPosition);

		private async Task InitHud(GameObject hero)
		{
			GameObject hud = await _gameFactory.CreateHud();
			hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
		}

		private static void CameraFollow(GameObject gameObject) => 
			Camera.main.GetComponent<CameraFollow>().Follow(gameObject);

		private LevelStaticData LevelStaticData() => 
			_staticData.ForLevel(SceneManager.GetActiveScene().name);
	}
}