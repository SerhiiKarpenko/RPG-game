using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Static_Data.Windows
{
	[CreateAssetMenu(fileName = "Window Data", menuName = "Static Data/Window Static Data")]
	public class WindowStaticData : ScriptableObject
	{
		public List<WindowConfig> WindowConfigs;
	}
}