using CodeBase.Logic;
using CodeBase.Logic.Enemy_Spawners;
using CodeBase.Static_Data;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
	[CustomEditor(typeof(LevelStaticData))]
	public class LevelStaticDataEditor : UnityEditor.Editor
	{
		private const string InitialPointTag = "InitialPoint";
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			LevelStaticData levelData = (LevelStaticData)target;
			
			if (GUILayout.Button("Collect"))
			{
				levelData.EnemySpawners =
					FindObjectsOfType<SpawnMarker>()
						.Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position))
						.ToList();

				levelData.LevelKey = SceneManager.GetActiveScene().name;
				
				levelData.InitialHeroPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
			}
			
			EditorUtility.SetDirty(target);
		}
	}
}