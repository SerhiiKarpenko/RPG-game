using CodeBase.Hero;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Physics_Debug;
using System.Linq;
using UnityEngine;


namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class Attack : MonoBehaviour
	{
		public float Cleavage = 0.5f;
		public float EffectiveDistance = 0.5f;
		[SerializeField] private EnemyAnimator _enemyAnimator;
		[SerializeField] private Transform _heroTransform;
		[SerializeField] private float _attackCooldown = 2f;
		[SerializeField] private float _damage = 10f;
		private float _currentAttackCooldown;
		private bool _isAttacking;
		private bool _attackIsActive;
		private int _layerMask;
		private Collider[] _hits = new Collider[1];
		private IGameFactory _factory;

		private void Awake()
		{
			_factory = AllServices.Container.Single<IGameFactory>();
			_factory.HeroCreated += OnHeroCreated;
			_layerMask = 1 << LayerMask.NameToLayer("Player");
		}

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartAttack();
		}

		private void OnAttack()
		{
			if (Hit(out Collider hit))
			{
				PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
				hit.transform.GetComponent<HeroHealth>().TakeDamage(_damage);
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
			transform.LookAt(_heroTransform);
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

		private void OnHeroCreated() => 
			_heroTransform = _factory.HeroGameObject.transform;

		private Vector3 StartPoint() => 
			new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;
	}
}
