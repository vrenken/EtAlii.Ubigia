// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if USE_ORIGINAL_CONTAINER

//#define CHECK_USAGE
namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public partial class Container
    {
        /// <inheritdoc />
        public void RegisterDecorator<TService, TDecorator>()
            where TService : class
            where TDecorator : TService
        {
            RegisterDecorator(typeof(TService), typeof(TDecorator));
        }

        private void RegisterDecorator(Type serviceType, Type decoratorType)
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

        private object DecorateInstanceIfNeeded(ContainerRegistration mapping, object instance, List<ContainerRegistration> involvedContainerRegistrations)
        {
            foreach (var decoratorMapping in mapping.Decorators)
            {
                if (mapping.Instance == null)
                {
                    var newInstance = CreateInstance(decoratorMapping.DecoratorType, decoratorMapping.ServiceType, instance, involvedContainerRegistrations);
                    mapping.Instance = newInstance;
                }
                instance = mapping.Instance;
            }
            return instance;
        }
    }
}

#endif
