using CodeBase.Camera_Logic;
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

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		private void OnLoaded()
		{
			var hero = _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));
			_gameFactory.CreateHud();
			CameraFollow(hero);
			_stateMachine.Enter<GameLoopState>();
		}

		private static void CameraFollow(GameObject gameObject)
		{
			Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
		}

		public void Exit() => 
			_loadingCurtain.Hide();
	}
}