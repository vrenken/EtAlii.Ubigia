// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.Diagnostics.CodeAnalysis;
    using EtAlii.xTechnology.MicroContainer;

    /// <summary>
    /// Use this as a base class to create configurable and extensible subsystem factories.
    /// </summary>

    [SuppressMessage(
        category: "Sonar Code Smell",
        checkId: "S2436:Reduce the number of generic parameters in the 'Factory' class to no more than the 2 authorized",
        Justification = "We cannot make this helper factory with less than 3 generic methods. Currently the change is too invasive to apply as all layers use this abstract class.")]
    public abstract class Factory<TInstance, TInstanceConfiguration, TExtension>
        where TInstanceConfiguration : IExtensible
        where TExtension: IExtension
    {
        /// <summary>
        /// Create a new TInstance factory instance for the given configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public TInstance Create(TInstanceConfiguration configuration)
        {
            var container = new Container();

            Initialize(container, configuration);

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

        /// <summary>
        /// Override this method and return the for the factory relevant IScaffolding instances.
        /// </summary>
        /// <returns></returns>
        protected abstract IScaffolding[] CreateScaffoldings(TInstanceConfiguration configuration);

        /// <summary>
        /// Override this method to configure the TInstance factory instance before it is returned.
        /// This allows for more advanced DI initialization approaches.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="services"></param>
        protected virtual void InitializeInstance(TInstance instance, IServiceCollection services)
        {
        }

        protected virtual void Initialize(IRegisterOnlyContainer container, TInstanceConfiguration configuration)
        {
        }
    }
}
