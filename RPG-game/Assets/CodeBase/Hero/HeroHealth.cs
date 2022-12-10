using CodeBase.Data;
using System;
using UnityEngine;

namespace CodeBase.Hero
{
	[RequireComponent(typeof(HeroAnimator))]
	public class HeroHealth : MonoBehaviour, ISavedProgress
	{
		[SerializeField] private HeroAnimator _heroAnimator;
		public Action HealthChange;
		private State _progressHeroState;

		public float MaxHealth 
		{
			get => _progressHeroState.MaxHeroHealth;
			set => _progressHeroState.MaxHeroHealth = value;
		}

		public float CurrentHealth
		{
			get => _progressHeroState.CurrentHeroHealth;
			set
			{
				_progressHeroState.CurrentHeroHealth = value;
				if (_progressHeroState.CurrentHeroHealth != value)
					HealthChange?.Invoke();
			}
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_progressHeroState = progress.HeroState;
			HealthChange?.Invoke();
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.HeroState.CurrentHeroHealth = CurrentHealth;
			progress.HeroState.MaxHeroHealth = MaxHealth;
		}

		public void TakeDamage(float damage)
		{
			if (CurrentHealth <= 0) return;
			CurrentHealth -= damage;
			_heroAnimator.PlayHit();
		}
	}
}