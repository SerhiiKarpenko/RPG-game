using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class StateFactory
    {
        private IInstantiator _instantiator;
        
        public StateFactory(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public TState CreateState<TState>() => 
            _instantiator.Instantiate<TState>();
    }
}