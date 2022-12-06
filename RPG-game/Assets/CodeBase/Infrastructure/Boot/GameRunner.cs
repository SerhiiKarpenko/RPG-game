using UnityEngine;

namespace CodeBase.Infrastructure.Boot
{
	public class GameRunner : MonoBehaviour
	{
		[SerializeField] private GameBootstrapper _gameBootstrapperPrefab;
		private void Awake()
		{
			var bootStrapper = FindObjectOfType<GameBootstrapper>();
			if( bootStrapper != null ) return;
			Instantiate(_gameBootstrapperPrefab);
		}
	}
}