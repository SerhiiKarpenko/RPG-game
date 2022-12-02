namespace CodeBase.Infrastructure
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string Main = "Main";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter(string sceneName) => 
			_sceneLoader.Load(sceneName);

		public void Exit()
		{
			
		}
	}
}