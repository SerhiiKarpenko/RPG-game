using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Services.Factory
{
	public interface IUIFactory : IService
	{
		public void CreateShop();
		public void CreateUIRoot();
	}
}