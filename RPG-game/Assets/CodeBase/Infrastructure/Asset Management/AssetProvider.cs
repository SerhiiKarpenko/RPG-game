using UnityEngine;

namespace CodeBase.Infrastructure.Asset_Management
{
	public class AssetProvider : IAssetProvider
	{
		public GameObject Instantiate(string path)
		{
			var heroPrefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(heroPrefab);
		}

		public GameObject Instantiate(string path, Vector3 at)
		{
			var heroPrefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(heroPrefab, at, Quaternion.identity);
		}

		public void Dispose() { }
	}
	
}