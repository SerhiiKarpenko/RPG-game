using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Logic
{
	public class LevelTransferTrigger : MonoBehaviour
	{
		private const string PlayerTag = "Player";
		
		[SerializeField] private string _transferTo;

		private bool _triggered;
		private IGameStateMachine _stateMachine;

		private void Awake() => 
			_stateMachine = AllServices.Container.Single<IGameStateMachine>();

		private void OnTriggerEnter(Collider other)
		{
			if (_triggered) return;
			
			if (other.CompareTag(PlayerTag))
			{
				_stateMachine.Enter<LoadLevelState, string>(_transferTo);
				_triggered = true;
			}
		}
	}
}