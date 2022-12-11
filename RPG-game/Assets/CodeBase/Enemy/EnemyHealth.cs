using CodeBase.Logic.Interfaces;
using System;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class EnemyHealth : MonoBehaviour, IHealth
	{
		public event Action OnHealthChange;
		[SerializeField] private float _maxHealth;
		[SerializeField] private float _currentHealth;
		[SerializeField] private EnemyAnimator _enemyAnimator;

		public float CurrentHealth
		{
			get => _currentHealth;
			set => _currentHealth = value;
		}

		public float MaxHealth
		{
			get => _maxHealth;
			set => _maxHealth = value;
		}

		public void TakeDamage(float damage)
		{
			_currentHealth -= damage;
			_enemyAnimator.PlayHit();
			OnHealthChange?.Invoke();
		}
	}
}