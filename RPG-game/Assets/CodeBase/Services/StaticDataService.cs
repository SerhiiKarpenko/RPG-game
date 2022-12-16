using CodeBase.Services;
using CodeBase.Static_Data.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Static_Data
{
	public class StaticDataService : IStaticDataService
	{
		private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;

		public void LoadMonsters()
		{
			_monsters = Resources
				.LoadAll<MonsterStaticData>("Static Data/Monsters")
				.ToDictionary(x => x.MonsterTypeId, x => x);
		}

		public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId) => 
			_monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData) 
				? staticData 
				: null;

		public void Dispose() { }
	}
}