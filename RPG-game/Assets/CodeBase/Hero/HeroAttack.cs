using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Interfaces;
using CodeBase.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
	[RequireComponent(typeof(HeroAnimator))]
	[RequireComponent(typeof(CharacterController))]
	public class HeroAttack : MonoBehaviour, ISavedProgressReader
	{
		[SerializeField] private HeroAnimator _heroAnimator;
		[SerializeField] private CharacterController _heroCharacterController;
		[SerializeField] private IInputService _inputService;
		private static int _layerMask;
		private Collider[] _hits = new Collider[3];
		private Stats _stats;
		private bool enabled = false;

		[Inject]
		public void Construct(IInputService inputService)
		{
			_inputService = inputService;
			_layerMask = 1 << LayerMask.NameToLayer("Hittable");
		}

		public void Initialize() => 
			enabled = true;

		private void Update()
		{
			if (!enabled)
			{
				return;
			}
			
			if (_inputService.IsAttackedButtonUp() && !_heroAnimator.IsAttacking)
				_heroAnimator.PlayAttack();
		}

		public void OnAttack()
		{
			for (int i = 0; i < Hit(); i++)
				_hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
		}

		private int Hit() => 
			Physics.OverlapSphereNonAlloc(StartPoint(), _stats.DamageRadius, _hits, _layerMask);

		private Vector3 StartPoint() =>
			new Vector3(transform.position.x, _heroCharacterController.center.y / 2, transform.position.z) + transform.forward;

		public void LoadProgress(PlayerProgress progress) => 
			_stats = progress.HeroStats;
	}
}