// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;
    using System.Reflection;

    public partial class Container
	{

        /// <inheritdoc />
        public void RegisterInitializer<TService>(Action<IServiceCollection, TService> initializer)
        {
            var wrapper = new Action<TService>(service => initializer(this, service));
            RegisterInitializer(wrapper);
        }

        /// <inheritdoc />
        public void RegisterInitializer<T>(Action<T> initializer)
        {
            var serviceType = typeof(T);

            // We want a stub in case the service type has not yet been registered.
            if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }

            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface: " + serviceType);
            }

            mapping.ImmediateInitializers.Add(o => initializer((T)o));
        }

        /// <summary>
        /// Registers a lazy initializer that will be called right after the requested root object has been constructed.
        /// This is useful and often needed for creating bidirectional object access which by theory are not possible in a normal DI tree.
        /// </summary>
        /// <param name="initializer"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type is not an interface.</exception>
        public void RegisterLazyInitializer<T>(Action<T> initializer)
        {
            var serviceType = typeof(T);

            // We want a stub in case the service type has not yet been registered.
            if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }

            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface: " + serviceType);
            }

            mapping.LazyInitializers.Add(o => initializer((T)o));
        }


        private void InitializeImmediately(ContainerRegistration mapping)
        {
            foreach (var initializer in mapping.ImmediateInitializers)
            {
                initializer(mapping.Instance!);
            }
        }

        private void InitializeLazy(ContainerRegistration mapping)
        {
            if (!mapping.IsLazyInitialized)
            {
                mapping.IsLazyInitialized = true;
                foreach (var lazyInitializer in mapping.LazyInitializers)
                {
                    lazyInitializer.Invoke(mapping.Instance!);
                }
            }
        }
    }
}

#endif
