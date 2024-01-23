using CodeBase.Infrastructure.Installers;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapperFactory : IGameBootstrapperFactory
    {
	    private DiContainer _diContainer;
	    
        public GameBootstrapperFactory(DiContainer diContainer) => 
	        _diContainer = diContainer;

        public void CreateBootstrapper() => 
	        _diContainer.InstantiatePrefabResource(InfrastructureAssetsPaths.GAMEBOOTSTRAPPER_PATH);
    }
}