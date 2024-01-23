using CodeBase.Infrastructure.Asset_Management;
using CodeBase.Infrastructure.Boot;
using CodeBase.Infrastructure.Services.Persistent_Progress;
using CodeBase.Infrastructure.Services.Save_Load;
using CodeBase.Services;
using CodeBase.Services.Ads;
using CodeBase.Services.IAP;
using CodeBase.Services.Input;
using CodeBase.Static_Data;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    // here we will ALL THINGS THAT NEED TO EXIST THROUGHT THE ALL GAME
    // this installer need to be applied as a component on a project context
    
    
    public class GameInstaller : MonoInstaller
    {
        public LoadingCurtain LoadingCurtain;
        
        public override void InstallBindings()
        {
            //register dependencies here
            BindGameBootstrapperFactory();
            BindGameFactory();
            BindUIFactory();
            BindGameStateMachineInstaller();
            BindCoroutineRunner();
            BindSceneLoader();
            BindInputService();
            BindStaticDataService();
            BindAssetProviderService();
            BindPersistentProgressService();
            BindSaveLoadService();
            BindIAPService();
            BindAdsService();
            BindRandomService();
            BindLoadingCurtain();
            BindWindowService();
            BindAppProvider();
        }

        private void BindGameBootstrapperFactory() => 
            Container.
                BindInterfacesTo<GameBootstrapperFactory>()
                .AsSingle();

        private void BindGameFactory() => 
            Container.
                BindInterfacesAndSelfTo<GameFactory>() // bind all interfaces that GameFactory class implements and self to GameFactory implementation
                .AsSingle();

        private void BindUIFactory() =>
            Container.
                BindInterfacesAndSelfTo<UIFactory>()
                .AsSingle();
        
        private void BindCoroutineRunner() =>
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefabResource(InfrastructureAssetsPaths.COROUTINE_RUNNER_PATH)
                .AsSingle();

        private void BindSceneLoader() => 
            Container
                .BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle();

        private void BindInputService()
        {
            
            Container
                .BindInterfacesAndSelfTo<StandaloneInputService>()
                .AsSingle().NonLazy();
            //
            // if (Application.isEditor)
            // {
            //     Container
            //         .BindInterfacesAndSelfTo<StandaloneInputService>()
            //         .AsSingle();
            // }
            // else
            // {
            //     Container
            //         .BindInterfacesAndSelfTo<MobileInputService>()
            //         .AsSingle();
            // }
        }

        private void BindStaticDataService() =>
            Container
                .BindInterfacesTo<StaticDataService>()
                .AsSingle();

        private void BindAssetProviderService() =>
            Container
                .BindInterfacesTo<AssetProvider>()
                .AsSingle();

        private void BindPersistentProgressService() =>
            Container.
                BindInterfacesTo<PersistentProgressService>().
                AsSingle();

        private void BindIAPService() =>
            Container
                .BindInterfacesTo<IAPService>()
                .AsSingle();

        private void BindAdsService() =>
            Container
                .BindInterfacesTo<AdsService>()
                .AsSingle();

        private void BindGameStateMachineInstaller() =>
            GameStateMachineInstaller.Install(Container);

        private void BindSaveLoadService() =>
            Container
                .BindInterfacesAndSelfTo<SaveLoadService>()
                .AsSingle();

        private void BindRandomService() =>
            Container
                .BindInterfacesAndSelfTo<RandomService>()
                .AsSingle();

        private void BindWindowService() =>
            Container
                .BindInterfacesAndSelfTo<WindowService>()
                .AsSingle();

        private void BindLoadingCurtain() => 
            Container.Bind<LoadingCurtain>()
                .FromComponentInNewPrefab(LoadingCurtain)
                .AsSingle();

        private void BindAppProvider() =>
            Container.BindInterfacesAndSelfTo<IAPProvider>().AsSingle();

    }
}
