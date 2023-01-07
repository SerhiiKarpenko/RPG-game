using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
	public abstract class WindowBase : MonoBehaviour
	{
		public Button CloseButton;

		private void Awake()
		{
			OnAwake();
		}

		protected virtual void OnAwake()
		{
			CloseButton.onClick.AddListener(()=>Destroy(gameObject));
		}
	}
}