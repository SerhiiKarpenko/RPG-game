using System;

namespace CodeBase.Data
{
	[Serializable]
	public class WorldData
	{
		public PositionOnLevel PositionOnLevel;
		public WorldData(string initialLevel)
		{
			PositionOnLevel = new PositionOnLevel(initialLevel);
		}
	}
}