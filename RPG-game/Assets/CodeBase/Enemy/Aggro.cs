using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class Aggro : MonoBehaviour
	{
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private Follow _follow;
		[SerializeField] private float _cooldown;
		private WaitForSeconds _followCooldown;
		private Coroutine _aggroCoroutine;
		private bool _hasAggroTarget;

		private void Start()
		{
			_followCooldown = new WaitForSeconds(_cooldown);
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
			SwitchFollowOff();
		}

		private void TriggerEnter(Collider obj)
		{
			if (!_hasAggroTarget)
			{
				_hasAggroTarget = true;
				StopAggroCoroutine();
				SwitchFollowOn();
			}
		}

		private void TriggerExit(Collider obj)
		{
			if (_hasAggroTarget)
			{
				_hasAggroTarget = false;
				_aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
			}
		}

		private IEnumerator SwitchFollowOffAfterCooldown()
		{
			yield return _followCooldown;
			SwitchFollowOff();
		}

		private void StopAggroCoroutine()
		{
			if (_aggroCoroutine != null) StopCoroutine(_aggroCoroutine);
		}

		private void SwitchFollowOn() => 
			_follow.enabled = true;

		private void SwitchFollowOff() => 
			_follow.enabled = false;
	}
}