using CodeBase.Logic.Interfaces;
using UnityEngine;

namespace CodeBase.UI.Elements
{
	public class ActorUI : MonoBehaviour
	{
		[SerializeField] private HealthBar _healthBar;
		private IHealth _health;

		public void Construct(IHealth health)
		{
			_health = health;
			_health.OnHealthChange += UpdateHealthBarP;
		}

		private void Start()
		{
			IHealth health = GetComponent<IHealth>();
			if (health != null)
				Construct(health); 
		}

		private void OnDestroy() => 
			_health.OnHealthChange -= UpdateHealthBarP;

		private void UpdateHealthBarP() => 
			_healthBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
	}
}