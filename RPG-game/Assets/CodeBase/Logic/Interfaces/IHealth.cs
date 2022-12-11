using System;

namespace CodeBase.Logic.Interfaces
{
	public interface IHealth
	{
		event Action OnHealthChange;
		float CurrentHealth { get; set; }
		float MaxHealth { get; set; }
		void TakeDamage(float damage);
	}
}