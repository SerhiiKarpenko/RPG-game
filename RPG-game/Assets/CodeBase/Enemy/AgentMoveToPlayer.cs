using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
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

		private void Start()
		{
			_gameFactory = AllServices.Container.Single<IGameFactory>();
			InitializeHeroTransform();
		}

		private void Update()
		{
			if (HeroInitialized() && HeroNotReached()) return;
			Agent.destination = _heroTransform.position;
		}

		private bool HeroInitialized() => 
			_heroTransform != null;

		private void HeroCreated() => 
			InitializeHeroTransform();

		private void InitializeHeroTransform()
		{
			if (_gameFactory.HeroGameObject != null)
			{
				_heroTransform = _gameFactory.HeroGameObject.transform;
			}
			else
			{
				_gameFactory.HeroCreated += HeroCreated;
			}
		}

		private bool HeroNotReached()
		{
			if (Vector3.Distance(transform.position, _heroTransform.position) <= _minimalDistance) return true;
			return false;
		}
	}
}
