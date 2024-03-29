// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;
    using System.Reflection;

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
            return _mappings.ContainsKey(serviceType);
        }

	    private bool HasRegistration(Type serviceType)
	    {
		    if(_mappings.TryGetValue(serviceType, out var registration))
	        {
                return registration.ConcreteType != null || registration.ConstructMethod != null;
            }
            return false;
        }
        /// <inheritdoc />
        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            var serviceType = typeof(TService);

#if DEBUG
            if (HasRegistration(serviceType))
            {
                throw new InvalidOperationException($"Service Type already registered: {serviceType}");
            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }
#endif
	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }
            mapping.ConcreteType = typeof(TImplementation);
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
            var serviceType = typeof(TService);
#if DEBUG
            if (HasRegistration(serviceType))
            {
                throw new InvalidOperationException($"Service Type already registered: {serviceType}");
            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }
#endif
	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }
            mapping.ConstructMethod = () => constructMethod()!;
            mapping.ConcreteType = typeof(TImplementation);
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
            var serviceType = typeof(TService);
#if DEBUG
            if (HasRegistration(serviceType))
            {
                throw new InvalidOperationException($"Service Type already registered: {serviceType}");

            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }
#endif
	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }
            mapping.ConstructMethod = () => constructMethod()!;
            mapping.ConcreteType = typeof(TService);
        }
    }
}

#endif
