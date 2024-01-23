using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Installers;
using CodeBase.Infrastructure.State_Machine;
using CodeBase.Infrastructure.State_Machine.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Boot
{
	public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		public LoadingCurtain CurtainPrefab;
		private Game _game;
		
		private GameStateMachine _gameStateMachine;
		private StateFactory _stateFactory;

		[Inject]
		private void Construct(GameStateMachine gameStateMachine, StateFactory stateFactory)
		{
			_stateFactory = stateFactory;
			_gameStateMachine = gameStateMachine;
		}
		
		private void Start()
		{
			_gameStateMachine.RegisterState(_stateFactory.CreateState<BootstrapState>());
			_gameStateMachine.RegisterState(_stateFactory.CreateState<LoadLevelState>());
			_gameStateMachine.RegisterState(_stateFactory.CreateState<LoadProgressState>());
			_gameStateMachine.RegisterState(_stateFactory.CreateState<GameLoopState>());
			
			_gameStateMachine.Enter<BootstrapState>();
			
			DontDestroyOnLoad(this);
		}
	}
}
