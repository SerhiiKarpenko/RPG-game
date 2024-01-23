using CodeBase.Services.Interface;
using CodeBase.Static_Data;
using CodeBase.Static_Data.Enums;
using CodeBase.Static_Data.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Services
{
	public interface IStaticDataService : IService
	{
		void LoadMonsters();
		MonsterStaticData ForMonster(MonsterTypeId monsterTypeId);
		LevelStaticData ForLevel(string sceneKey);
		WindowConfig ForWindow(WindowId windowId);
	}
}