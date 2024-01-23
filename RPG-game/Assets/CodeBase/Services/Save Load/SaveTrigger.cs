﻿using UnityEngine;
using Zenject;

namespace CodeBase.Services.Save_Load
{
	public class SaveTrigger : MonoBehaviour
	{
		[SerializeField] private BoxCollider _collider;
		private ISaveLoadService _saveLoadService;
		private bool _saveZoneTriggered = false;

		[Inject]
		public void Construct(ISaveLoadService saveLoadService) => 
			_saveLoadService = saveLoadService;

		private void Awake()
		{
			if (_saveZoneTriggered)
			{
				gameObject.SetActive(false);
				return;
			}

			//_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
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