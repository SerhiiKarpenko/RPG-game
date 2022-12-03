
using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.Persistent_Progress
{
	public class PersistentProgressService : IPersistentProgressService
	{
		public PlayerProgress Progress { get; set; }
		public void Dispose() { }
	}
}