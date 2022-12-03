using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public interface IGameFactory : IService
	{
		GameObject CreateHero(GameObject at);
		void CreateHud();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		void CleanUp();
	}
}