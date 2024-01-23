using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Services;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.RandomService;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemy
{
	public class LootSpawner : MonoBehaviour
	{
		[SerializeField] private EnemyDeath _enemyDeath;
		private IRandomService _random;
		private IGameFactory _factory;
		private int _lootMin;
		private int _lootMax;
		
		[Inject]
		public void Construct(IGameFactory factory, IRandomService random)
		{
			_factory = factory;
			_random = random;
		}

		private void Start()
		{
			_enemyDeath.Happened += SpawnLoot;
		}

		private async void SpawnLoot()
		{
			LootPiece loot = await _factory.CreateLoot();
			loot.transform.position = transform.position;
			Loot lootItem = GenerateLoot();
			loot.Initialize(lootItem);
		}

		private Loot GenerateLoot()
		{
			return new Loot
			{
				Value = _random.Next(_lootMin, _lootMax)
			};
		}

		public void SetLoot(int min, int max)
		{
			_lootMin = min;
			_lootMax = max;
		}
	}
}