using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;

		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter()
		{
			RegisterServices();
			_sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
		}

		private void EnterLoadLevel()
		{
			
		}

		private void RegisterServices()
		{
			Game.InputService = RegisterInputService();
		}

		public void Exit()
		{
			
		}
		
		private static IInputService RegisterInputService()
		{
			if (Application.isEditor)
				return new StandaloneInputService();
			return new MobileInputService();
		}

	}
}