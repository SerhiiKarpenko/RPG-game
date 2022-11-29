using CodeBase.Services.Input;
using UnityEngine.Device;

namespace CodeBase.Infrastructure
{
	public class Game
	{
		public static IInputService InputService;

		public Game()
		{
			RegisterInputService();
		}

		private static void RegisterInputService()
		{
			if (Application.isEditor)
				InputService = new StandaloneInputService();
			else
				InputService = new MobileInputService();
		}
	}
}
