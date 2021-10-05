// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer
{
    /// <summary>
    /// Use this as a base class to create a non-configurable nor extensible subsystem factory.
    /// </summary>
    public abstract class Factory<TInstance>
    {
        /// <summary>
        /// Create a new TInstance factory instance.
        /// </summary>
        /// <returns></returns>
        public TInstance Create()
        {
            var container = new Container();

            Initialize(container);

            var scaffoldings = CreateScaffoldings();


            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            var instance = container.GetInstance<TInstance>();
            InitializeInstance(instance, container);
            return instance;
        }

        /// <summary>
        /// Override this method and return the for the factory relevant IScaffolding instances.
        /// </summary>
        /// <returns></returns>
        protected abstract IScaffolding[] CreateScaffoldings();

        /// <summary>
        /// Override this method to configure the TInstance factory instance before it is returned.
        /// This allows for more advanced DI initialization approaches.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="container"></param>
        protected virtual void InitializeInstance(TInstance instance, Container container)
        {
        }

        /// <summary>
        /// Override this method to initialize the TInstance factory.
        /// </summary>
        /// <param name="container"></param>
        protected virtual void Initialize(Container container)
        {
        }
    }
}
