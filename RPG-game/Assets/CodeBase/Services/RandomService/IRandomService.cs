using CodeBase.Services.Interface;

namespace CodeBase.Services.RandomService
{
	public interface IRandomService : IService
	{
		int Next(int min, int max);
	}
}