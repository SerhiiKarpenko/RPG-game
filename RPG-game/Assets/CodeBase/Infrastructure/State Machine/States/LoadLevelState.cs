using CodeBase.Camera_Logic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _progressService;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
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
			GameObject hero = InitHero();
			InitHud(hero);
			CameraFollow(hero);
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