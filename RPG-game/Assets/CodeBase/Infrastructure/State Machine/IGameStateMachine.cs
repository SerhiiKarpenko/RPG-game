using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure
{
	public interface IGameStateMachine : IService
	{
		void Enter<TState>() where TState : class, IState;
		void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;

		public void RegisterState<TState>(TState state) where TState : class, IExitableState;
	}
}