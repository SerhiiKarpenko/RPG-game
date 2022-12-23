using CodeBase.Infrastructure;
using System;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class LootSpawner : MonoBehaviour
	{
		[SerializeField] private EnemyDeath _enemyDeath;
		private IGameFactory _factory;

		public void Construct(IGameFactory factory)
		{
			_factory = factory;
		}

		private void Start()
		{
			_enemyDeath.Happened += SpawnLoot;
		}

		private void SpawnLoot()
		{
			GameObject loot = _factory.CreateLoot();
			loot.transform.position = transform.position;
		}
	}
}