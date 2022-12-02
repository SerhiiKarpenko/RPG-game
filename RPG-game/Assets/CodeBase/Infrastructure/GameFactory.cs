using CodeBase.Infrastructure.Asset_Management;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;

		public GameFactory(IAssetProvider assets)
		{
			_assets = assets;
		}
		
		public GameObject CreateHero(GameObject at) => 
			_assets.Instantiate(AssetPath.HeroPath, at: at.transform.position);

		public void CreateHud() => 
			_assets.Instantiate(AssetPath.HudPath);
	}
}