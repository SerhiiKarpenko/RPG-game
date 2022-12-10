using CodeBase.Hero;
using System;
using UnityEngine;

namespace CodeBase.UI
{
	public class ActorUI : MonoBehaviour
	{
		[SerializeField] private HealthBar _healthBar;
		private HeroHealth _heroHealth;

		private void OnDestroy() => 
			_heroHealth.HealthChange -= UpdateHealthBarP;

		public void Construct(HeroHealth health)
		{
			_heroHealth = health;
			_heroHealth.HealthChange += UpdateHealthBarP;
		}
		
		private void UpdateHealthBarP() => 
			_healthBar.SetValue(_heroHealth.CurrentHealth, _heroHealth.MaxHealth);
	}
}