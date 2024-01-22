using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Hero
{
	public class HeroMove : MonoBehaviour, ISavedProgress
	{
		public CharacterController CharacterController;
		public float MovementSpeed;
		private IInputService _inputService;
		private Camera _camera;

		private void Awake()
		{
			//_inputService = AllServices.Container.Single<IInputService>();
			_camera = Camera.main;
		}

		[Inject]
		public void Construct(IInputService inputService) => 
			_inputService = inputService;

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

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(),  transform.position.AsVectorData());
		}

		public void LoadProgress(PlayerProgress progress)
		{
			if (CurrentLevel() != progress.WorldData.PositionOnLevel.LevelName) return;
			
			Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
			if (savedPosition == null) return;
			
			Warp(to: savedPosition);
		}

		private void Warp(Vector3Data to)
		{
			CharacterController.enabled = false;
			transform.position = to.AsUnityVector().AddY(CharacterController.height);
			CharacterController.enabled = true;
		}

		private static string CurrentLevel() => 
			SceneManager.GetActiveScene().name;
	}
}
