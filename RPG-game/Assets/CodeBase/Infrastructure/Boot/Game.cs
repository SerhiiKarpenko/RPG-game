﻿using CodeBase.Infrastructure.State_Machine;

namespace CodeBase.Infrastructure.Boot
{
	public class Game
	{
		public GameStateMachine StateMachine;

		public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
		{
			//StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
		}
	}
}
