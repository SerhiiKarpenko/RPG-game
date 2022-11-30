using System;
using UnityEngine;

namespace CodeBase.Infrastructure 
{ 
	public class GameBootstrapper : MonoBehaviour
	{
		private Game _game;
		private void Awake()
		{
			_game = new Game();
			_game.StateMachine.Enter<BootstrapState>();
			DontDestroyOnLoad(this);
		}
	}

	public class SceneLoader
	{
		public void LoadScene(string name, Action onLoaded = null)
		{
			
		}
	}
}
