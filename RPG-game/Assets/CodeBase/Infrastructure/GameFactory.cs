using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Logic.Enemy_Spawners;
using CodeBase.Logic.Interfaces;
using CodeBase.Services;
using CodeBase.Static_Data;
using CodeBase.Static_Data.Enums;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;
using OpenWindowButton = CodeBase.UI.Elements.OpenWindowButton;

namespace CodeBase.Infrastructure
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private readonly IRandomService _random;
		private readonly IPersistentProgressService _persistentProgressService;
		private readonly IWindowService _windowService;

		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

		public GameFactory(IAssetProvider assets, IStaticDataService staticData, IRandomService random,
			IPersistentProgressService persistentProgressService, IWindowService windowService)
		{
			_assets = assets;
			_staticData = staticData;
			_random = random;
			_persistentProgressService = persistentProgressService;
			_windowService = windowService;
		}

		~GameFactory() => 
			Dispose();

		public async Task WarmUp()
		{
			await _assets.Load<GameObject>(AssetAddress.Loot);
			await _assets.Load<GameObject>(AssetAddress.Spawner);
		}

		public async Task<GameObject> CreateHero(Vector3 at)
		{
			HeroGameObject = await InstantiateRegisteredAsync(AssetAddress.HeroPath, at);
			return HeroGameObject;
		}

		private GameObject HeroGameObject { get; set; }

		public async Task<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
		{
			MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeId);

			GameObject prefab = await _assets.Load<GameObject>(monsterData.PrefabReference);
			GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
			
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

		public async Task<LootPiece> CreateLoot()
		{
			GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Loot);
			LootPiece lootPiece = InstantiateRegistered(prefab)
				.GetComponent<LootPiece>();
			
			lootPiece.Construct(_persistentProgressService.Progress.WorldData);
			return lootPiece;
		}

		public async Task<GameObject> CreateHud()
		{
			GameObject hud = await InstantiateRegisteredAsync(AssetAddress.HudPath);
			hud.GetComponentInChildren<LootCounter>().Construct(_persistentProgressService.Progress.WorldData);

			foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
			{
				openWindowButton.Construct(_windowService);
			}
			
			return hud;
		}

		public async Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
		{
			GameObject prefab = await _assets.Load<GameObject>(AssetAddress.Spawner);
			SpawnPoint spawner = InstantiateRegistered(prefab, at)
				.GetComponent<SpawnPoint>();
			
			spawner.Construct(this);
			spawner.Id = spawnerId;
			spawner.MonsterTypeId = monsterTypeId;
		}

		public void Dispose() => 
			CleanUp();

		public void CleanUp()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
			
			_assets.CleanUp();
		}

		private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
		{
			GameObject gameObject = await _assets.Instantiate(prefabPath, at: at);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
		{
			GameObject gameObject = await _assets.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
		{
			GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiateRegistered(GameObject prefab)
		{
			GameObject gameObject = Object.Instantiate(prefab);
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