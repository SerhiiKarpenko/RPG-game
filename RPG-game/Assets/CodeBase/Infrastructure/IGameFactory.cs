using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using CodeBase.Static_Data.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public interface IGameFactory : IService
	{
		GameObject CreateHero(GameObject at);
		GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);

		GameObject CreateHud();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		void CleanUp();
		void Register(ISavedProgressReader progressReader);
		LootPiece CreateLoot();
	}
}