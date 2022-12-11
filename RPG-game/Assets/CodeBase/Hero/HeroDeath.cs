using UnityEngine;

namespace CodeBase.Hero
{
	[RequireComponent(typeof(HeroHealth))]
	[RequireComponent(typeof(HeroMove))]
	[RequireComponent(typeof(HeroAnimator))]
	public class HeroDeath : MonoBehaviour
	{
		[SerializeField] private HeroHealth _heroHealth;
		[SerializeField] private HeroMove _heroMove;
		[SerializeField] private HeroAttack _heroAttack;
		[SerializeField] private HeroAnimator _heroAnimator;
		[SerializeField] private GameObject _deathFx;
		private bool _isDead;

		private void Start() => 
			_heroHealth.OnHealthChange += HealthChange;

		private void OnDestroy() => 
			_heroHealth.OnHealthChange -= HealthChange;

		private void HealthChange()
		{
			if (!_isDead && _heroHealth.CurrentHealth <= 0)
				Die();
		}

		private void Die()
		{
			_isDead = true;
			_heroMove.enabled = false;
			_heroAttack.enabled = false;
			_heroAnimator.PlayDeath();
			Instantiate(_deathFx, transform.position, Quaternion.identity);
		}
	}
}