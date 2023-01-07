using CodeBase.Camera_Logic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Services;
using CodeBase.Static_Data;
using CodeBase.UI;
using CodeBase.UI.Elements;
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

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
			_staticData = staticData;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() => 
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();
			InformProgressReaders();
			_stateMachine.Enter<GameLoopState>();
		}

		private void InformProgressReaders()
		{
			foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
			{
				progressReader.LoadProgress(_progressService.Progress);
			}
		}

		private void InitGameWorld()
		{
			InitSpawners();
			GameObject hero = InitHero();
			InitHud(hero);
			CameraFollow(hero);
		}

		private void InitSpawners()
		{
			string sceneKey = SceneManager.GetActiveScene().name;
			LevelStaticData levelData = _staticData.ForLevel(sceneKey);
			foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
			{
				_gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeId);
			}
		}

		private GameObject InitHero() => 
			_gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));

		private void InitHud(GameObject hero)
		{
			GameObject hud = _gameFactory.CreateHud();
			hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
		}

		private static void CameraFollow(GameObject gameObject)
		{
			Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
		}
	}
}