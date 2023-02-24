using CodeBase.Infrastructure.Services;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Asset_Management
{
	public interface IAssetProvider : IService
	{
		Task<GameObject> Instantiate(string path);
		Task<GameObject> Instantiate(string path, Vector3 at);
		Task<T> Load<T>(AssetReference assetReference) where T : class;
		void CleanUp();
		Task<T> Load<T>(string address)where T : class;
		void Initialize();
	}
}