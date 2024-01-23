using CodeBase.Infrastructure;
using CodeBase.Infrastructure.State_Machine;
using CodeBase.Infrastructure.State_Machine.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic
{
	public class LevelTransferTrigger : MonoBehaviour
	{
		private const string PlayerTag = "Player";
		
		[SerializeField] private string _transferTo;

		private bool _triggered;
		private IGameStateMachine _stateMachine;
		
		[Inject]
		public void Construct(IGameStateMachine gameStateMachine) => 
			_stateMachine = gameStateMachine;

		private void OnTriggerEnter(Collider other)
		{
			if (_triggered) 
				return;
			
			if (other.CompareTag(PlayerTag))
			{
				_stateMachine.Enter<LoadLevelState, string>(_transferTo);
				_triggered = true;
			}
		}
	}
}