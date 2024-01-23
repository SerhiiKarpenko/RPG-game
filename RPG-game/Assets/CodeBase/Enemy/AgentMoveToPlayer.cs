using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factories;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
	public class AgentMoveToPlayer : Follow
	{
		public NavMeshAgent Agent;
		private Transform _heroTransform;
		private const float _minimalDistance = 1;
		private IGameFactory _gameFactory;

		private void Update()
		{
			SetDestinationForAgent();
		}

		private void SetDestinationForAgent()
		{
			if (HeroNotReached()) return;
			Agent.destination = _heroTransform.position;
		}

		public void Construct(Transform heroTransform) => 
			_heroTransform = heroTransform;


		private bool HeroNotReached()
		{
			if (Vector3.Distance(transform.position, _heroTransform.position) <= _minimalDistance) return true;
			return false;
		}
	}
}
