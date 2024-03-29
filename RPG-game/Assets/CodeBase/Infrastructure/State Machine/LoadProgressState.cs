﻿using CodeBase.Data;
using CodeBase.Infrastructure.State_Machine.States;
using CodeBase.Services.Persistent_Progress;
using CodeBase.Services.Save_Load;

namespace CodeBase.Infrastructure.State_Machine
{
	public class LoadProgressState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _saveLoadService;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_saveLoadService = saveLoadService;
		}

		public void Enter()
		{
			LoadProgressOrInitNew();
			_gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.LevelName);
		}

		public void Exit()
		{
			
		}

		private void LoadProgressOrInitNew() =>
			_progressService.Progress = 
				_saveLoadService.LoadProgress() 
				?? NewProgress();

		private PlayerProgress NewProgress()
		{
			var progress = new PlayerProgress(initialLevel: "Graveyard");
			progress.HeroState.MaxHeroHealth = 50f;
			progress.HeroStats.Damage = 1f;
			progress.HeroStats.DamageRadius = 0.5f;
			progress.HeroState.ResetHealth();
			return progress;
		}
	}
}