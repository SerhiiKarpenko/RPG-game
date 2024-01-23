using CodeBase.Data;
using CodeBase.Services.Interface;

namespace CodeBase.Services.Save_Load
{
	public interface ISaveLoadService : IService
	{
		void SaveProgress();
		PlayerProgress LoadProgress();
	}
}