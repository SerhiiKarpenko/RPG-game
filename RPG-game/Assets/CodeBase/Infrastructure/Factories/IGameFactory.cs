using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Services;
using CodeBase.Services.Interface;
using CodeBase.Static_Data.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
	public interface IGameFactory : IService
	{
		Task<GameObject> CreateHero(Vector3 at);
		Task<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent);

		Task<GameObject> CreateHud();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		void CleanUp();
		Task<LootPiece> CreateLoot();
		Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
		Task WarmUp();
	}
}