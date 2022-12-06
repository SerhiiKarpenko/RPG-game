using CodeBase.Hero;
using CodeBase.Infrastructure.Asset_Management;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;
		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

		public GameFactory(IAssetProvider assets)
		{
			_assets = assets;
		}
		
		~GameFactory() => 
			Dispose();

		public GameObject CreateHero(GameObject at) => 
			InstantiateRegistered(AssetPath.HeroPath, at.transform.position);

		public void CreateHud() => 
			InstantiateRegistered(AssetPath.HudPath);

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

		private void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);
			
			ProgressReaders.Add(progressReader);
		}
	}
}