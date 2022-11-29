using UnityEngine;

namespace CodeBase.Camera_Logic
{
	public class CameraFollow : MonoBehaviour
	{
		public int Distance;
		public float RotationAngleX;
		public float OffsetY;
		[SerializeField] private Transform _following;

		private void LateUpdate()
		{
			if (_following == null) return;
			Quaternion rotation = Quaternion.Euler(RotationAngleX, 0f, 0f);
			Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();
			transform.rotation = rotation;
			transform.position = position;
		}

		public void Follow(GameObject following) => 
			_following = following.transform;

		private Vector3 FollowingPointPosition()
		{
			Vector3 followingPosition = _following.position;
			followingPosition.y += OffsetY;
			return followingPosition;
		}
	}
}