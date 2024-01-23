using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.State_Machine;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<StateFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            
            Debug.Log("GameStateMachineInstaller working");
        }
    }
}