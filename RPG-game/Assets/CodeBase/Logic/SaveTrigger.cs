using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Save_Load;
using System;
using UnityEngine;

namespace CodeBase.Logic
{
	public class SaveTrigger : MonoBehaviour
	{
		[SerializeField] private BoxCollider _collider;
		private ISaveLoadService _saveLoadService;
		
		private void Awake()
		{
			_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
		}

		private void OnTriggerEnter(Collider other)
		{
			_saveLoadService.SaveProgress();
			Debug.Log("Progress Saved.");
			gameObject.SetActive(false);
		}

		private void OnDrawGizmos()
		{
			if (_collider == null) return;
			
			Gizmos.color = new Color32(30, 200, 30, 130);
			Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
		}
	}
}