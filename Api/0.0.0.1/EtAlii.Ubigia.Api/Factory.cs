namespace EtAlii.Ubigia.Api
{
    using EtAlii.xTechnology.MicroContainer;

    /// <summary>
    /// Use this as a base class to create configurable and extensible subsystem factories. 
    /// </summary>
    public abstract class Factory<TInstance, TInstanceConfiguration, TExtension>
        where TInstanceConfiguration : Configuration
        where TExtension: IExtension
    {
        public TInstance Create(TInstanceConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = CreateScaffoldings(configuration);

            
            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.GetExtensions<TExtension>())
            {
                extension.Initialize(container);
            }

            var instance = container.GetInstance<TInstance>();
            InitializeInstance(instance, container);
            return instance;
        }

        protected abstract IScaffolding[] CreateScaffoldings(TInstanceConfiguration configuration);

        protected virtual void InitializeInstance(TInstance instance, Container container)
        {
        }
    }
    
    /// <summary>
    /// Use this as a base class to create a non-configurable nor extensible subsystem factory. 
    /// </summary>
    public abstract class Factory<TInstance>
    {
        public TInstance Create()
        {
            var container = new Container();

            var scaffoldings = CreateScaffoldings();

            
            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            var instance = container.GetInstance<TInstance>();
            InitializeInstance(instance, container);
            return instance;
        }

        protected abstract IScaffolding[] CreateScaffoldings();

        protected virtual void InitializeInstance(TInstance instance, Container container)
        {
        }
    }
}