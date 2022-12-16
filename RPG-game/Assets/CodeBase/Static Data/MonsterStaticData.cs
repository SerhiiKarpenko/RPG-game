using CodeBase.Static_Data.Enums;
using UnityEngine;

namespace CodeBase.Static_Data
{
	[CreateAssetMenu(fileName = "Monster Data", menuName = "Static Data/Monster")]
	public class MonsterStaticData : ScriptableObject
	{
		[Range(1, 100)] public int Hp;
		[Range(1f, 30f)] public float Damage;
		[Range(0.5f, 1f)] public float EffectiveDistance = 0.5f;
		[Range(0.5f, 1f)] public float Cleavage;
		[Range(1f, 10f)] public float MoveSpeed;
		public MonsterTypeId MonsterTypeId;
		public GameObject Prefab;
	}
}