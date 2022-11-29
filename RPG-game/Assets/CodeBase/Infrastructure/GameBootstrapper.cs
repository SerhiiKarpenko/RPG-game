using UnityEngine;

namespace CodeBase.Infrastructure 
{ 
	public class GameBootstrapper : MonoBehaviour
	{
		private Game _game;
		private void Awake()
		{
			_game = new Game();
			DontDestroyOnLoad(this);
		}
	}
}
