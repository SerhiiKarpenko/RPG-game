using CodeBase.Infrastructure.Services;
using System.Threading.Tasks;

namespace CodeBase.UI.Services.Factory
{
	public interface IUIFactory : IService
	{
		public void CreateShop();
		public Task CreateUIRoot();
	}
}