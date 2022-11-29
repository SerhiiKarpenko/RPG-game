using CodeBase.Camera_Logic;
using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using System;
using UnityEngine;

namespace CodeBase.Hero
{
	public class HeroMove : MonoBehaviour
	{
		public CharacterController CharacterController;
		public float MovementSpeed;
		private IInputService _inputService;
		private Camera _camera;

		private void Awake()
		{
			_inputService = Game.InputService;
		}

		private void Start()
		{
			_camera = Camera.main;
			CameraFollow();
		}
		
		private void Update()
		{
			Vector3 movementVector = Vector3.zero;
			if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
			{
				movementVector = _camera.transform.TransformDirection(_inputService.Axis);
				movementVector.y = 0;
				movementVector.Normalize();

				transform.forward = movementVector;
			}

			movementVector += Physics.gravity;
			CharacterController.Move( MovementSpeed * movementVector * Time.deltaTime);
		}

		private void CameraFollow() => 
			_camera.GetComponent<CameraFollow>().Follow(gameObject);
	}
}
