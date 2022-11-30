using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
	public class SceneLoader
	{
		private readonly ICoroutineRunner _coroutineRunner;

		public SceneLoader(ICoroutineRunner coroutineRunner) => 
			_coroutineRunner = coroutineRunner;

		public void Load(string name, Action onLoaded = null) =>
			_coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

		private IEnumerator LoadScene(string name, Action onLoaded = null)
		{
			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);
			while (!waitNextScene.isDone)
				yield return null;
			onLoaded?.Invoke();
		}
	}
}