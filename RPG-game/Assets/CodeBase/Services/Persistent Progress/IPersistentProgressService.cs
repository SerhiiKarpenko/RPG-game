using CodeBase.Data;
using CodeBase.Services.Interface;

namespace CodeBase.Services.Persistent_Progress
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress Progress { get; set; }
	}
}