﻿using CodeBase.Hero;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemy
{
	public class RotateToHero : Follow
	{
		[SerializeField] private float _speed;
		private HeroMove _heroTransform;
		private IGameFactory _gameFactory;
		private Vector3 _positionToLook;
		
		private void Update()
		{
			if (!HeroInitialized()) return;
			LookAtHero();
		}

		[Inject]
		public void Construct(HeroMove heroMove) => 
			_heroTransform = heroMove;

		private bool HeroInitialized() => 
			_heroTransform != null;


		private void LookAtHero()
		{
			UpdatePositionToLook();
			transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
		}

		private void UpdatePositionToLook()
		{
			Vector3 headingToHero = _heroTransform.transform.position - transform.position;
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