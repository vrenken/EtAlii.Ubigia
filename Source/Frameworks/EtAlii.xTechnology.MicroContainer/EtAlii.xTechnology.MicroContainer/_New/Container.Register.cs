// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    public partial class Container
    {

        public bool HasRegistration<TService>()
            where TService : class
        {
            var serviceType = typeof(TService);
#if DEBUG
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }
#endif
            var hasDescriptor = _collection.Any(service => service.ServiceType == serviceType);
            return hasDescriptor;
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
            if (HasRegistration<TService>())
            {
                throw new InvalidOperationException($"Service Type already registered: {serviceType}");
            }
#endif
            _collection.AddSingleton<TService, TImplementation>();
        }

        /// <inheritdoc />
        public void Register<TService, TImplementation>(Func<IServiceCollection, TImplementation> constructMethod)
            where TService : class
            where TImplementation : class, TService
        {
            var wrapper = new Func<TImplementation>(() => constructMethod(this));
            Register<TService, TImplementation>(wrapper);
        }

        /// <inheritdoc />
        public void Register<TService, TImplementation>(Func<TImplementation> constructMethod)
            where TService : class
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
            if (HasRegistration<TService>())
            {
                throw new InvalidOperationException($"Service Type already registered: {serviceType}");
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
            if (HasRegistration<TService>())
            {
                throw new InvalidOperationException($"Service Type already registered: {serviceType}");
            }
#endif

            _collection.AddSingleton(_ => constructMethod());
        }
    }
}

#endif
