using CodeBase.Data;
using CodeBase.Logic.Interfaces;
using System;
using UnityEngine;

namespace CodeBase.Hero
{
	[RequireComponent(typeof(HeroAnimator))]
	public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
	{
		public event Action OnHealthChange;
		
		[SerializeField] private HeroAnimator _heroAnimator;
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
				if (_progressHeroState.CurrentHeroHealth != value)
				{
					_progressHeroState.CurrentHeroHealth = value;
					OnHealthChange?.Invoke();
				}
			}
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_progressHeroState = progress.HeroState;
			OnHealthChange?.Invoke();
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