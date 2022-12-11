using System;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	[RequireComponent(typeof(EnemyHealth))]
	public class EnemyDeath : MonoBehaviour
	{
		public event Action Happened;
		[SerializeField] private EnemyAnimator _enemyAnimator;
		[SerializeField] private EnemyHealth _enemyHealth;
		[SerializeField] private Follow _follow;
		[SerializeField] private GameObject _deathFx;

		private void Start() => 
			_enemyHealth.OnHealthChange += HealthChange;

		private void OnDestroy() =>
			_enemyHealth.OnHealthChange -= HealthChange;
			

		private void HealthChange()
		{
			if (_enemyHealth.CurrentHealth <= 0)
				Die();
		}

		private void Die()
		{
			_follow.enabled = false;
			_enemyHealth.OnHealthChange -= HealthChange;
			_enemyAnimator.PlayDeath();
			SpawnDeathFx();
			Destroy(gameObject, 2f);
			Happened?.Invoke();
		}

		private void SpawnDeathFx() => 
			Instantiate(_deathFx, transform.position, Quaternion.identity);
	}
}