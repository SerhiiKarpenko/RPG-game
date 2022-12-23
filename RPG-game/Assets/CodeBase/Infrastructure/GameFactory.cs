using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Logic.Interfaces;
using CodeBase.Services;
using CodeBase.Static_Data;
using CodeBase.Static_Data.Enums;
using CodeBase.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private readonly IRandomService _random;
		
		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

		public GameFactory(IAssetProvider assets, IStaticDataService staticData, IRandomService random)
		{
			_assets = assets;
			_staticData = staticData;
			_random = random;
		}

		~GameFactory() => 
			Dispose();

		public GameObject CreateHero(GameObject at)
		{
			HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
			return HeroGameObject;
		}

		private GameObject HeroGameObject { get; set; }

		public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
		{
			MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeId);
			GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);
			
			IHealth health = monster.GetComponent<IHealth>();
			health.CurrentHealth = monsterData.Hp;
			health.MaxHealth = monsterData.Hp;
			
			monster.GetComponent<ActorUI>().Construct(health);
			monster.GetComponent<AgentMoveToPlayer>()?.Construct(HeroGameObject.transform);
			monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
			LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
			lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
			lootSpawner.Construct(this, _random);

			Attack monsterAttack = monster.GetComponent<Attack>();
			monsterAttack.Construct(HeroGameObject.transform);
			monsterAttack.Damage = monsterData.Damage;
			monsterAttack.Cleavage = monsterData.Cleavage;
			monsterAttack.EffectiveDistance = monsterData.EffectiveDistance;
			
			monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);
			
			return monster;
		}

		public GameObject CreateLoot() => 
			InstantiateRegistered(AssetPath.Loot);

		public GameObject CreateHud()
		{
			GameObject hud = InstantiateRegistered(AssetPath.HudPath);
			return hud;
		}

		public void Dispose() => 
			CleanUp();

		public void CleanUp()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
		{
			GameObject gameObject = _assets.Instantiate(prefabPath, at: at);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			GameObject gameObject = _assets.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}

		public void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);
			
			ProgressReaders.Add(progressReader);
		}
	}
}