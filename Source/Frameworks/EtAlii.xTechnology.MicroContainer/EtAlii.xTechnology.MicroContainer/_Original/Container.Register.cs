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
        public void Register<TService, TImplementation>()
            where TImplementation : TService
        {
            var serviceType = typeof(TService);
            if (HasRegistration(serviceType))
            {
                throw new InvalidOperationException($"Service Type already registered: {serviceType}");
            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }

	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }
            mapping.ConcreteType = typeof(TImplementation);
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
            where TImplementation : TService
        {
            var serviceType = typeof(TService);
            if (HasRegistration(serviceType))
            {
                throw new InvalidOperationException($"Service Type already registered: {serviceType}");
            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }

	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }
            mapping.ConstructMethod = () => constructMethod()!;
            mapping.ConcreteType = typeof(TImplementation);
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
        public void Register<TService>(Func<TService> constructMethod)
        {
            var serviceType = typeof(TService);
            if (HasRegistration(serviceType))
            {
                throw new InvalidOperationException($"Service Type already registered: {serviceType}");

            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }

	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }
            mapping.ConstructMethod = () => constructMethod()!;
            mapping.ConcreteType = typeof(TService);
        }

        /// <inheritdoc />
        public void RegisterDecorator(Type serviceType, Type decoratorType)
	    {
            // We want a stub in case the service type has not yet been registered.
		    if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }

            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }
            if (mapping.Decorators.Any(d => d.DecoratorType == decoratorType))
            {
                throw new InvalidOperationException($"Decorator Type already registered: {decoratorType}");
            }

            if (!serviceType.GetTypeInfo().IsAssignableFrom(decoratorType.GetTypeInfo()))
            {
                throw new InvalidOperationException($"Unable to apply Decorator Type to service Type: {decoratorType}");
            }

            var decoratorRegistration = new DecoratorRegistration
            {
                DecoratorType = decoratorType,
                ServiceType = serviceType,
            };
            mapping.Decorators.Add(decoratorRegistration);
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

	    private bool HasRegistration(Type serviceType)
	    {
		    if(_mappings.TryGetValue(serviceType, out var registration))
	        {
                return registration.ConcreteType != null || registration.ConstructMethod != null;
            }
            return false;
        }
    }
}

#endif
