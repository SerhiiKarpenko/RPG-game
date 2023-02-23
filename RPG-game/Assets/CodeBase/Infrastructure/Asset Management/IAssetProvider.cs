using CodeBase.Infrastructure.Services;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Asset_Management
{
	public interface IAssetProvider : IService
	{
		GameObject Instantiate(string path);
		GameObject Instantiate(string path, Vector3 at);
		Task<T> Load<T>(AssetReference assetReference) where T : class;
		void CleanUp();
	}
}