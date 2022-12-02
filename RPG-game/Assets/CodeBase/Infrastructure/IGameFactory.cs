using UnityEngine;

namespace CodeBase.Infrastructure
{
	public interface IGameFactory
	{
		GameObject CreateHero(GameObject at);
		void CreateHud();
	}
}