using UnityEngine;

namespace CodeBase.Physics_Debug
{
	public static class PhysicsDebug
	{
		public static void DrawDebug(Vector3 worldPosition, float radius, float seconds)
		{
			Debug.DrawRay(worldPosition, radius * Vector3.up, Color.red, seconds);
			Debug.DrawRay(worldPosition, radius * Vector3.down, Color.red, seconds);
			Debug.DrawRay(worldPosition, radius * Vector3.left, Color.red, seconds);
			Debug.DrawRay(worldPosition, radius * Vector3.right, Color.red, seconds);
			Debug.DrawRay(worldPosition, radius * Vector3.forward, Color.red, seconds);
			Debug.DrawRay(worldPosition, radius * Vector3.back, Color.red, seconds);
		}
	}
}