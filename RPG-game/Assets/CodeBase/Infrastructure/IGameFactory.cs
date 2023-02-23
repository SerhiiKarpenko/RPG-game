using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using CodeBase.Static_Data.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public interface IGameFactory : IService
	{
		GameObject CreateHero(Vector3 at);
		Task<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent);

		GameObject CreateHud();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		void CleanUp();
		LootPiece CreateLoot();
		void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
	}
}