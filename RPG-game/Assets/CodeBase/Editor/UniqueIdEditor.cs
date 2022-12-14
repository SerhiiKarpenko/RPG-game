using CodeBase.Logic;
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(UniqueId))]
	public class UniqueIdEditor : UnityEditor.Editor
	{
		private void OnEnable()
		{
			var uniqueId = (UniqueId) target;
			if (string.IsNullOrEmpty(uniqueId.Id)) Generate(uniqueId);
		}

		private void Generate(UniqueId uniqueId)
		{
			uniqueId.Id = Guid.NewGuid().ToString();
			
			if (Application.isPlaying) return;
			EditorUtility.SetDirty(uniqueId);
			EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
		}
	}
}