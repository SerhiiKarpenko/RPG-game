using System;

namespace CodeBase.Data
{
	[Serializable]
	public class State
	{
		public float CurrentHeroHealth;
		public float MaxHeroHealth;
		public void ResetHealth() => 
			CurrentHeroHealth = MaxHeroHealth;
	}
}