using CodeBase.Data;
using System.Collections;
using CodeBase.Services.Persistent_Progress;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Zenject;

namespace CodeBase.Enemy
{
	public class LootPiece : MonoBehaviour
	{
		public GameObject Skull;
		public GameObject PickupFxPrefab;
		public GameObject PickupPopup;
		public TextMeshPro LootText;
		private Loot _loot;
		private bool _picked;
		//private WorldData _worldData;
		private IPersistentProgressService _persistentProgressService;

		[Inject]
		public void Construct(IPersistentProgressService persistentProgressService)
		{
			_persistentProgressService = persistentProgressService;
			//_worldData = worldData;
		}

		public void Initialize(Loot loot) => 
			_loot = loot;

		private void OnTriggerEnter(Collider other) => 
			Pickup();

		private void Pickup()
		{
			if (_picked) return;
			_picked = true;
			UpdateWorldData();
			HideSkull();
			PlayPickupFx();
			ShowText();
			StartCoroutine(StartDestroyTimer());
		}

		private void UpdateWorldData() => 
			_persistentProgressService.Progress.WorldData.LootData.Collect(_loot);

		private void HideSkull() => 
			Skull.SetActive(false);

		private void PlayPickupFx() => 
			Instantiate(PickupFxPrefab, transform.position, Quaternion.identity);

		private void ShowText()
		{
			LootText.text = $"{_loot.Value}";
			PickupPopup.SetActive(true);
		}

		private IEnumerator StartDestroyTimer()
		{
			yield return new WaitForSeconds(1.5f);
			Destroy(gameObject);
		}
	}
}