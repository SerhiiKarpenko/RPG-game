using CodeBase.Infrastructure.State_Machine.States;
using CodeBase.Services;
using CodeBase.Services.Interface;

namespace CodeBase.Infrastructure.State_Machine
{
	public interface IGameStateMachine : IService
	{
		void Enter<TState>() where TState : class, IState;
		void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;

		public void RegisterState<TState>(TState state) where TState : class, IExitableState;
	}
}