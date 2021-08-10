// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This container is build to be simple and pure. We don't want to assign too much 'lifetime' responsibilities
    /// to our DI framework. Most lifetime behavior can be solved way easier without a container.
    /// </summary>
    public partial class Container
    {
        private readonly ServiceCollection _collection = new();
        private ServiceProvider? _serviceProvider;

        /// <summary>
        /// Instantiates and returns an instance that got configured for the specified interface.
        /// If the instance requires any constructor parameters these will also get instantiated and injected.
        ///
        /// The container works in a way that it tries to create the constructor-injected objects dependency tree.
        /// During this creation it will run all requested initializations immediately after the construction of
        /// a single object, and after the root object has been created it will run all lazy registrations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">In case one of the objects in the DI graph cannot be initialized.
        /// For example due to a missing registration or when a cyclic dependency has been defined.</exception>
        public T GetInstance<T>()
        {
            _serviceProvider ??= _collection.BuildServiceProvider();
            var instance = _serviceProvider.GetService<T>();
            if (instance == null)
            {
                throw new InvalidOperationException($"Service Type could not be instantiated: {typeof(T)}");
            }

            return instance;
        }

        /// <summary>
        /// Register an concrete implementation type to be instantiated wherever the service interface is being used as
        /// a constructor parameter.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type has already been registered or when the service type is not an interface.</exception>
        public void Register<TService, TImplementation>()
            where TService: class
            where TImplementation : class, TService
        {
#if DEBUG
            if (_serviceProvider != null)
            {
                throw new InvalidOperationException($"Service Provider already instantiated");
            }
            var serviceType = typeof(TService);
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }
#endif
            _collection.AddSingleton<TService, TImplementation>();
        }

        /// <summary>
        /// Register an object instantiation function that will be used to provide the concrete instance wherever the service interface is being used as
        /// a constructor parameter.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type has already been registered or when the service type is not an interface.</exception>
        public void Register<TService, TImplementation>(Func<TImplementation> constructMethod)
            where TService : class
            where TImplementation : TService
        {
#if DEBUG
            if (_serviceProvider != null)
            {
                throw new InvalidOperationException($"Service Provider already instantiated");
            }
            var serviceType = typeof(TService);
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }
#endif

            _collection.AddSingleton<TService>(_ => constructMethod());
        }

        /// <summary>
        /// Register an object instantiation function that will be used to provide an instance wherever the service interface is being used as
        /// a constructor parameter.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type has already been registered or when the service type is not an interface.</exception>
        public void Register<TService>(Func<TService> constructMethod)
            where TService : class
        {
#if DEBUG
            if (_serviceProvider != null)
            {
                throw new InvalidOperationException($"Service Provider already instantiated");
            }
            var serviceType = typeof(TService);
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }
#endif

            _collection.AddSingleton(_ => constructMethod());
        }

        private object GetInstance(IServiceProvider provider, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationType != null)
            {
                return ActivatorUtilities.GetServiceOrCreateInstance(provider, descriptor.ImplementationType!);
            }

            return descriptor.ImplementationFactory!(provider);
        }
    }
}

#endif
