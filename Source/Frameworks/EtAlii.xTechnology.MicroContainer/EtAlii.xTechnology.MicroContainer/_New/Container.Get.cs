// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;

    public partial class Container
    {
        /// <inheritdoc />
        [return: NotNull]
        public T GetInstance<T>()
        {
            _serviceProvider ??= _collection.BuildServiceProvider();

            var instance = _serviceProvider.GetService<T>();

            // Remark: The null check is disabled in release mode, still the method should never return null as DI is
            // build to compose the whole application logical structure.
#if DEBUG
            if (instance == null)
            {
                throw new InvalidOperationException($"Service Type could not be instantiated: {typeof(T)}");
            }
#endif
            foreach (var initializer in _initializers)
            {
                initializer.Initialize(_serviceProvider);
            }
#if DEBUG
            return instance;
#else
            return instance!;
#endif
        }

        private object GetInstance(IServiceProvider provider, ServiceDescriptor descriptor)
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
