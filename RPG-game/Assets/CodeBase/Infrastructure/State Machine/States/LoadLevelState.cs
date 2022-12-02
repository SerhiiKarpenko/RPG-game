using CodeBase.Camera_Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";
		private const string HeroHero = "Hero/hero";
		private const string HudPath = "Hud/Hud";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		private void OnLoaded()
		{
			GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
			GameObject hero = Instantiate(HeroHero, at: initialPoint.transform.position);
			Instantiate(HudPath);
			CameraFollow(hero);
			_stateMachine.Enter<GameLoopState>();
		}

		private static void CameraFollow(GameObject gameObject)
		{
			Camera.main.GetComponent<CameraFollow>().Follow(gameObject);
		}

		private static GameObject Instantiate(string path)
		{
			var heroPrefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(heroPrefab);
		}
		
		private static GameObject Instantiate(string path, Vector3 at)
		{
			var heroPrefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(heroPrefab, at, Quaternion.identity);
		}

		public void Exit() => 
			_loadingCurtain.Hide();
	}
}