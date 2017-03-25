namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Structure;

    public class MicroContainerDependencyResolver : IDependencyResolver
    {
        private readonly IWebApiComponent[] _components;
        private readonly ISelector<Type, Func<Type, object>> _resolverSelector;

        public MicroContainerDependencyResolver(Container container, IWebApiComponent[] components)
        {
            _components = components;
            _resolverSelector = new Selector<Type, Func<Type, object>>()
                .Register(type => type == typeof(AuthenticateController), type => new AuthenticateController())
                // Additional registrations.
                .Register(type => type == typeof(IdentifierBinder), type => new IdentifierBinder())
                .Register(type => type == typeof(IdentifiersBinder), type => new IdentifiersBinder())
                // And finally usage of the instances from the container.
                .Register(type => type.Assembly == typeof(MicroContainerDependencyResolver).Assembly, container.GetInstance)
                .Register(type => true, type => null);
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            // First check if any of the components is aware of a service.
            foreach (var component in _components)
            {
                object serviceInstance;
                if (component.TryGetService(serviceType, out serviceInstance))
                {
                    return serviceInstance;
                }
            }

            // Nope, probably basic classes. Let's handle them ourselves.
            var resolver = _resolverSelector.Select(serviceType);
            return resolver(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            serviceType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var resolver = _resolverSelector.Select(serviceType);
            var result = (IEnumerable<object>)resolver(serviceType);
            return result ?? Enumerable.Empty<object>();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}