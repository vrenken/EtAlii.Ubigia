// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This container is build to be simple and pure. We don't want to assign too much 'lifetime' responsibilities
    /// to our DI framework. Most lifetime behavior can be solved way easier without a container.
    /// </summary>
    public partial class Container
    {

        /// <summary>
        /// Registers an initializer that will be called right after an object has been constructed.
        /// This is useful and often needed for creating bidirectional object access which by theory are not possible in a normal DI tree.
        /// </summary>
        /// <param name="initializer"></param>
        /// <typeparam name="TService"></typeparam>
        /// <exception cref="InvalidOperationException">In case the service type is not an interface.</exception>
        public void RegisterInitializer<TService>(Action<TService> initializer)
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
            var oldDescriptor = _collection.SingleOrDefault(service => service.ServiceType == serviceType);
            if (oldDescriptor == null)
            {
                throw new InvalidOperationException($"Service Type initialization could not be registered: {serviceType}");
            }

            var index = _collection.IndexOf(oldDescriptor);

            var newFactory = new Func<IServiceProvider, object>(provider =>
            {
                var instance = (TService)GetInstance(provider, oldDescriptor);
                initializer(instance);
                return instance!;
            });

            _collection[index] = ServiceDescriptor.Describe(serviceType, newFactory, ServiceLifetime.Singleton);
        }
    }
}

#endif
