using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class BootstrapState : IState
	{
		private readonly GameStateMachine _stateMachine;

		public BootstrapState(GameStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}

		public void Enter()
		{
			RegisterServices();
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