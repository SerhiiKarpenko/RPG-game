namespace CodeBase.Infrastructure
{
	public interface IState
	{
		void Enter();
		void Exit();
	}
}