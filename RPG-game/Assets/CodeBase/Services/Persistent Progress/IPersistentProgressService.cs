using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Persistent_Progress
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress Progress { get; set; }
	}
}