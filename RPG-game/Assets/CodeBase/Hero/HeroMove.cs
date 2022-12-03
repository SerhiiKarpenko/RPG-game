using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
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
			_inputService = AllServices.Container.Single<IInputService>();
			_camera = Camera.main;
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


	}
}
