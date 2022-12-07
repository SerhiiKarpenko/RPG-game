using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class RotateToHero : Follow
	{
		[SerializeField] private float _speed;
		private Transform _heroTransform;
		private IGameFactory _gameFactory;
		private Vector3 _positionToLook;

		private void Start()
		{
			_gameFactory = AllServices.Container.Single<IGameFactory>();
			if (HeroInitialized())
				InitializeHeroTransform();
			else
				_gameFactory.HeroCreated += InitializeHeroTransform;
		}

		private void Update()
		{
			if (!HeroInitialized()) return;
			LookAtHero();
		}

		private bool HeroInitialized() => 
			_heroTransform != null;

		private void HeroCreated() => 
			InitializeHeroTransform();

		private void InitializeHeroTransform() => 
			_heroTransform = _gameFactory.HeroGameObject.transform;

		private void LookAtHero()
		{
			UpdatePositionToLook();
			transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
		}

		private void UpdatePositionToLook()
		{
			Vector3 headingToHero = _heroTransform.position - transform.position;
			_positionToLook = new Vector3(headingToHero.x, transform.position.y, headingToHero.z);
		}

		private Quaternion SmoothedRotation(Quaternion currentRotation, Vector3 positionToLook) =>
			Quaternion.Lerp(currentRotation, TargetLookRotation(positionToLook), SpeedFactor());

		private static Quaternion TargetLookRotation(Vector3 positionToLook) => 
			Quaternion.LookRotation(positionToLook);

		private float SpeedFactor() => 
			_speed * Time.deltaTime;
	}
}