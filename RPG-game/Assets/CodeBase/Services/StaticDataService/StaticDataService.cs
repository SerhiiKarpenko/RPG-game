using System.Collections.Generic;
using System.Linq;
using CodeBase.Static_Data;
using CodeBase.Static_Data.Enums;
using CodeBase.Static_Data.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Services.StaticDataService
{
	public class StaticDataService : IStaticDataService
	{
		private const string StaticDataWindowsPath = "Static Data/Windows/Window Data";
		private const string StaticDataLevelsPath = "Static Data/Levels";
		private const string StaticDataMonstersPath = "Static Data/Monsters";
		private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
		private Dictionary<string, LevelStaticData> _levels;
		private Dictionary<WindowId, WindowConfig> _windowConfigs;

		public void LoadMonsters()
		{
			_monsters = Resources
				.LoadAll<MonsterStaticData>(StaticDataMonstersPath)
				.ToDictionary(x => x.MonsterTypeId, x => x);
			
			_levels = Resources
				.LoadAll<LevelStaticData>(StaticDataLevelsPath)
				.ToDictionary(x => x.LevelKey, x => x);
			
			_windowConfigs = Resources
				.Load<WindowStaticData>(StaticDataWindowsPath)
				.WindowConfigs
				.ToDictionary(x => x.WindowId, x => x);
		}

		public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId) => 
			_monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData) 
				? staticData 
				: null;

		public LevelStaticData ForLevel(string sceneKey) =>
			_levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData) 
				? levelStaticData 
				: null;

		public WindowConfig ForWindow(WindowId windowId) =>
			_windowConfigs.TryGetValue(windowId, out WindowConfig levelStaticData) 
				? levelStaticData 
				: null;

		public void Dispose() { }
	}
}