using System.Threading.Tasks;
using CodeBase.Services;
using CodeBase.Services.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Asset_Management
{
	public interface IAssetProvider : IService
	{
		Task<GameObject> Instantiate(string path);
		Task<GameObject> Instantiate(string path, Vector3 at);
		Task<GameObject> Instantiate(string address, Transform under);
		Task<T> Load<T>(AssetReference assetReference) where T : class;
		void CleanUp();
		Task<T> Load<T>(string address)where T : class;
		UniTask Initialize();
	}
}