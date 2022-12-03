using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Save_Load
{
	public interface ISaveLoadService : IService
	{
		void SaveProgress();
		PlayerProgress LoadProgress();
	}
}