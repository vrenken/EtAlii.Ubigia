// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Reflection;

    /// <summary>
    /// This container is build to be simple and pure. We don't want to assign too much 'lifetime' responsibilities
    /// to our DI framework. Most lifetime behavior can be solved way easier without a container.
    /// </summary>
    public partial class Container
    {
        /// <inheritdoc />
        public void RegisterInitializer<TService>(Action<IServiceCollection, TService> initializer)
        {
            var wrapper = new Action<TService>(service => initializer(this, service));
            RegisterInitializer(wrapper);
        }

        /// <inheritdoc />
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
            _initializers.Add(new ServiceInitializer<TService>(initializer));
        }
    }
}

#endif
