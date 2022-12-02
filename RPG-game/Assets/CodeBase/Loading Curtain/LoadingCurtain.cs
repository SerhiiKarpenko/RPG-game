using System.Collections;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
	[SerializeField] private CanvasGroup _canvasGroup;

	private void Awake() => 
		DontDestroyOnLoad(this);

	public void Show()
	{
		gameObject.SetActive(true);
		_canvasGroup.alpha = 1f;
	}

	public void Hide() => 
		StartCoroutine(FadeIn());

	private IEnumerator FadeIn()
	{
		while (_canvasGroup.alpha > 0)
		{
			_canvasGroup.alpha -= 0.03f;
			yield return new WaitForSeconds(0.03f);
		}
		gameObject.SetActive(false);
	}
}
