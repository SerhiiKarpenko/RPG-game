using CodeBase.Services.Input;

namespace CodeBase.Infrastructure.Boot
{
	public class Game
	{
		public static IInputService InputService;
		public GameStateMachine StateMachine;

		public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
		{
			StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain);
		}
	}
}
