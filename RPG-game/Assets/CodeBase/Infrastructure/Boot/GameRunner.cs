using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Boot
{
	public class GameRunner : MonoBehaviour
	{
		private IGameBootstrapperFactory _gameBootstrapperFactory;

		[Inject]
		public void Construct(IGameBootstrapperFactory gameBootstrapperFactory) => 
			_gameBootstrapperFactory = gameBootstrapperFactory;

		private void Awake()
		{
			var bootStrapper = FindObjectOfType<GameBootstrapper>();
			if( bootStrapper != null ) return;
			_gameBootstrapperFactory.CreateBootstrapper();
		}
	}
}