using CodeBase.Infrastructure.Services;
using CodeBase.Static_Data;
using CodeBase.Static_Data.Enums;

namespace CodeBase.Services
{
	public interface IStaticDataService : IService
	{
		void LoadMonsters();
		MonsterStaticData ForMonster(MonsterTypeId monsterTypeId);
	}
}