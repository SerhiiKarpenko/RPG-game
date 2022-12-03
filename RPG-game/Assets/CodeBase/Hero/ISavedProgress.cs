using CodeBase.Data;

namespace CodeBase.Hero
{
	public interface ISavedProgressReader
	{
		void LoadProgress(PlayerProgress progress);
	}

	public interface ISavedProgress : ISavedProgressReader
	{
		void UpdateProgress(PlayerProgress progress);
	}
}