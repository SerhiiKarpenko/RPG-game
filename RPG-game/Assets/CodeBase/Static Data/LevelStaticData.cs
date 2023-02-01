using CodeBase.Logic;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Static_Data
{
	[CreateAssetMenu(fileName = "Level Data", menuName = "Static Data/Level")]
	public class LevelStaticData : ScriptableObject
	{
		public string LevelKey;
		public List<EnemySpawnerData> EnemySpawners;
		public Vector3 InitialHeroPosition;
	}
}