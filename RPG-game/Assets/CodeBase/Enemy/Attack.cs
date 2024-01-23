using CodeBase.Infrastructure;
using CodeBase.Logic.Interfaces;
using CodeBase.Physics_Debug;
using System.Linq;
using CodeBase.Hero;
using UnityEngine;
using Zenject;


namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class Attack : MonoBehaviour
	{
		public float Cleavage = 0.5f;
		public float EffectiveDistance = 0.5f;
		public float Damage = 10f;
		[SerializeField] private EnemyAnimator _enemyAnimator;
		[SerializeField] private Transform _heroTransform;
		[SerializeField] private float _attackCooldown = 2f;
		private float _currentAttackCooldown;
		private bool _isAttacking;
		private bool _attackIsActive;
		private int _layerMask;
		private Collider[] _hits = new Collider[1];
		private IGameFactory _factory;

		private void Awake()
		{
			_layerMask = 1 << LayerMask.NameToLayer("Player");
		}

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartAttack();
		}

		public void Construct(Transform transform) => 
			_heroTransform = transform;

		private void OnAttack()
		{
			if (Hit(out Collider hit))
			{
				PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
				hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
			}
		}

		public void EnableAttack() => 
			_attackIsActive = true;

		public void DisableAttack() => 
			_attackIsActive = false;

		private bool Hit(out Collider hit)
		{
			int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);
			hit = _hits.FirstOrDefault();
			return hitCount > 0;
		}

		private void OnAttackEnded()
		{
			_currentAttackCooldown = _attackCooldown;
			_isAttacking = false;
		}

		private void StartAttack()
		{
			transform.LookAt(_heroTransform.transform);
			_enemyAnimator.PlayAttack();
			_isAttacking = true;
		}

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_currentAttackCooldown -= Time.deltaTime;
		}

		private bool CanAttack() => 
			_attackIsActive && !_isAttacking && CooldownIsUp();

		private bool CooldownIsUp() => 
			_currentAttackCooldown <= 0;


		private Vector3 StartPoint() => 
			new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;
	}
}
