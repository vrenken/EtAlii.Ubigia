namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Linq;
    using System.Reflection;

    public partial class Container
	{
        public void Register<TService, TImplementation>()
            where TImplementation : TService
        {
            var serviceType = typeof(TService);
            if (HasRegistration(serviceType))
            {
                throw new InvalidOperationException("Service Type already registered: " + serviceType);
            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface: " + serviceType);
            }

	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }
            mapping.ConcreteType = typeof(TImplementation);
        }

        public void Register<TService, TImplementation>(Func<TImplementation> constructMethod)
            where TImplementation : TService
        {
            var serviceType = typeof(TService);
            if (HasRegistration(serviceType))
            {
                throw new InvalidOperationException("Service Type already registered: " + serviceType);
            }
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface: " + serviceType);
            }

	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }
            mapping.ConstructMethod = () => constructMethod();
            mapping.ConcreteType = typeof(TImplementation);
        }

        public void Register<TService>(Func<TService> constructMethod)
        {
            var serviceType = typeof(TService);
            if (HasRegistration(serviceType))
            {
                throw new InvalidOperationException("Service Type already registered: " + serviceType);
            
            }
            var ti = serviceType.GetTypeInfo();
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface: " + serviceType);
            }

	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }
            mapping.ConstructMethod = () => constructMethod();
            mapping.ConcreteType = typeof(TService);
        }

        public void RegisterDecorator(Type serviceType, Type decoratorType)
	    {
            // We want a stub in case the service type has not yet been registered.
		    if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }

            //if (!_mappings.ContainsKey(serviceType))
            //{
            //    throw new InvalidOperationException("Service Type not yet registered: " + serviceType);
            //}
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface: " + serviceType);
            }
            if (mapping.Decorators.Any(d => d.DecoratorType == decoratorType))
            {
                throw new InvalidOperationException("Decorator Type already registered: " + decoratorType);
            }

            if (!serviceType.GetTypeInfo().IsAssignableFrom(decoratorType.GetTypeInfo()))
            {
                throw new InvalidOperationException("Unable to apply Decorator Type to Service Type: " + decoratorType);
            }

            var decoratorRegistration = new DecoratorRegistration
            {
                DecoratorType = decoratorType,
                ServiceType = serviceType,
            };
            mapping.Decorators.Add(decoratorRegistration);
        }

        public void RegisterInitializer<T>(Action<T> initializer)
        {
            var serviceType = typeof(T);

            // We want a stub in case the service type has not yet been registered.
	        if (!_mappings.TryGetValue(serviceType, out var mapping))
            {
                _mappings[serviceType] = mapping = new ContainerRegistration();
            }

            //if (!_mappings.ContainsKey(serviceType))
            //{
            //    throw new InvalidOperationException("Service Type not registered: " + serviceType);
            //}
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException("Service Type should be an interface: " + serviceType);
            }

            mapping.Initializers.Add(o => initializer((T)o));
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
