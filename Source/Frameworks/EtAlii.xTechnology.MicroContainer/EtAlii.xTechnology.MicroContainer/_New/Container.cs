// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This container is build to be simple and pure. We don't want to assign too much 'lifetime' responsibilities
    /// to our DI framework. Most lifetime behavior can be solved way easier without a container.
    /// </summary>
    public partial class Container : IRegisterOnlyContainer, IServiceCollection
    {
        private readonly ServiceCollection _collection = new();
        private ServiceProvider? _serviceProvider;
        private readonly List<ServiceInitializer> _initializers = new();

        /// <inheritdoc />
        public T GetInstance<T>()
        {
            _serviceProvider ??= _collection.BuildServiceProvider();
            var instance = _serviceProvider.GetService<T>();
            if (instance == null)
            {
                throw new InvalidOperationException($"Service Type could not be instantiated: {typeof(T)}");
            }

            foreach (var initializer in _initializers)
            {
                initializer.Initialize(_serviceProvider);
            }
            return instance;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void Register<TService, TImplementation>(Func<IServiceCollection, TImplementation> constructMethod)
            where TService : class
            where TImplementation : TService
        {
            var wrapper = new Func<TImplementation>(() => constructMethod(this));
            Register<TService, TImplementation>(wrapper);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void Register<TService>(Func<IServiceCollection, TService> constructMethod)
            where TService : class
        {
            var wrapper = new Func<TService>(() => constructMethod(this));
            Register(wrapper);
        }

        /// <inheritdoc />
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

        internal object GetInstance(IServiceProvider provider, ServiceDescriptor descriptor)
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
