using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
	[Serializable]
	public class PlayerProgress
	{
		public State HeroState;
		public WorldData WorldData;

		public PlayerProgress(string initialLevel)
		{
			WorldData = new WorldData(initialLevel);
			HeroState = new State();

		}
	}
}