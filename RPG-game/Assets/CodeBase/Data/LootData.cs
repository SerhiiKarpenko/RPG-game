using System;

namespace CodeBase.Data
{
	[Serializable]
	public class LootData
	{
		public int Collected;
		public Action Changed;

		public void Collect(Loot loot)
		{
			Collected += loot.Value;
			Changed?.Invoke();
		}
	}
}