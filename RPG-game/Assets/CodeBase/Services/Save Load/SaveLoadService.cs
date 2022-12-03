using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Save_Load
{
	class SaveLoadService : ISaveLoadService
	{
		private const string ProgressKey = "Progress";
		private readonly IPersistentProgressService _progressService;
		private readonly IGameFactory _gameFactory;

		public SaveLoadService(IPersistentProgressService progressService ,IGameFactory gameFactory)
		{
			_progressService = progressService;
			_gameFactory = gameFactory;
		}

		public void SaveProgress()
		{
			foreach (ISavedProgress progressWriters in _gameFactory.ProgressWriters)
				progressWriters.UpdateProgress(_progressService.Progress);
			
			PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
		}

		public PlayerProgress LoadProgress() =>
			PlayerPrefs.GetString(ProgressKey)?
				.ToDeserialized<PlayerProgress>();

		public void Dispose()
		{
			
		}
	}
}