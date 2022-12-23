using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class LootPiece : MonoBehaviour
	{
		private Loot _loot;
		private bool _picked;
		private WorldData _worldData;

		public void Construct(WorldData worldData)
		{
			_worldData = worldData;
		}

		public void Initialize(Loot loot)
		{
			_loot = loot;
		}
		
		private void OnTriggerEnter(Collider other) => 
			Pickup();

		private void Pickup()
		{
			if (_picked) return;
			
			_picked = true;
			_worldData.LootData.Collect(_loot);

		}
	}
}