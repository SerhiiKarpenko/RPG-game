using System.Threading.Tasks;
using CodeBase.Services;
using CodeBase.Services.Interface;

namespace CodeBase.UI.Services.Factory
{
	public interface IUIFactory : IService
	{
		public void CreateShop();
		public Task CreateUIRoot();
	}
}