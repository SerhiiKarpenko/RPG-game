using CodeBase.Services;
using CodeBase.Services.Interface;

namespace CodeBase.UI.Services.Windows
{
	public interface IWindowService : IService
	{
		void Open(WindowId windowId);
	}
}