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
        /// <inheritdoc />
        public void RegisterDecorator(Type serviceType, Type decoratorType)
        {
#if DEBUG
            if (!serviceType.GetTypeInfo().IsInterface)
            {
                throw new InvalidOperationException($"Service Type should be an interface: {serviceType}");
            }
#endif
            var oldDescriptor = _collection.SingleOrDefault(service => service.ServiceType == serviceType);
            if (oldDescriptor == null)
            {
                throw new InvalidOperationException($"Service Type decorator could not be registered: {decoratorType}");
            }

            var index = _collection.IndexOf(oldDescriptor);

            var newFactory = new Func<IServiceProvider, object>(provider =>
            {
                var decoree = GetInstance(provider, oldDescriptor);
                var decorator = CreateDecorator(provider, decoratorType, decoree, serviceType);
                return decorator;
            });

            _collection[index] = ServiceDescriptor.Describe(serviceType, newFactory, ServiceLifetime.Singleton);
        }

        private object CreateDecorator(IServiceProvider provider, Type decoratorType, object decoree, Type serviceType)
        {
            var constructors = decoratorType.GetTypeInfo().DeclaredConstructors
                .Where(c => !c.IsStatic && c.IsPublic)
                .ToArray();

            if (constructors.Length > 1)
            {
                throw new InvalidOperationException("Multiple public constructors are not allowed for type: " + decoratorType);
            }
            if (constructors.Length == 0)
            {
                throw new InvalidOperationException("No constructors found for type: " + decoratorType);
            }

            var constructor = constructors[0];
            var parameters = constructor.GetParameters();

            var instances = new object[parameters.Length];

            var parameterIndex = 0;
            try
            {
                for (parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
                {
                    var parameterType = parameters[parameterIndex].ParameterType;
                    if (parameterType == serviceType)
                    {
                        instances[parameterIndex] = decoree;
                    }
                    else
                    {
                        instances[parameterIndex] = ActivatorUtilities.GetServiceOrCreateInstance(provider, parameterType);
                    }
                }
                return constructor.Invoke(instances);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Unable to create instance of type: {decoratorType} due to issues with parameter: {parameterIndex}", e);
            }
        }
    }
}

#endif
