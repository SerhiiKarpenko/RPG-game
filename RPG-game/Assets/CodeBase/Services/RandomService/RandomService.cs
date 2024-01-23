using UnityEngine;

namespace CodeBase.Services.RandomService
{
	public class RandomService : IRandomService
	{
		public int Next(int min, int max) => 
			Random.Range(min, max);

		public void Dispose() { }
	}
}