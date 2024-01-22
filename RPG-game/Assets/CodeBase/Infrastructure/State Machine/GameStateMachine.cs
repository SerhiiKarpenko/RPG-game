using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Infrastructure.Services.Save_Load;
using CodeBase.Services;
using CodeBase.UI.Services.Factory;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure
{
	public class GameStateMachine : IGameStateMachine
	{
		private readonly Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
		{
			_states = new Dictionary<Type, IExitableState>();
			// {
			// 	[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
			// 	
			// 	[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(), 
			// 		services.Single<IPersistentProgressService>(), services.Single<IStaticDataService>(), services.Single<IUIFactory>()),
			// 	
			// 	[typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
			// 	[typeof(GameLoopState)] = new GameLoopState(this),
			// };
		}
		
		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}

		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
		{
			TState state = ChangeState<TState>();
			state.Enter(payload);
		}

		public void RegisterState<TState>(TState state) where TState : class, IExitableState => 
			_states.Add(typeof(TState), state);

		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			_activeState?.Exit();
			TState state = GetState<TState>();
			_activeState = state;
			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState => 
			_states[typeof(TState)] as TState;

		public void Dispose()
		{
			
		}
	}
}