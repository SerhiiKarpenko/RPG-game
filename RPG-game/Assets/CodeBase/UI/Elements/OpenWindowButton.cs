using CodeBase.UI.Services.Windows;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class OpenWindowButton : MonoBehaviour
	{
		public Button OpenButton;

		public WindowId WindowId;
		private IWindowService _windowService;

		public void Construct(IWindowService windowService) => 
			_windowService = windowService;

		private void Awake() => 
			OpenButton.onClick.AddListener(Open);

		private void Open() => 
			_windowService.Open(WindowId);
		
	}
}