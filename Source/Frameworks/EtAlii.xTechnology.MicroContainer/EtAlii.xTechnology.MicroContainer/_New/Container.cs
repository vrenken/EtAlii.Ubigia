// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This container is build to be simple and pure. We don't want to assign too much 'lifetime' responsibilities
    /// to our DI framework. Most lifetime behavior can be solved way easier without a container.
    /// </summary>
    [DebuggerNonUserCode]
    public partial class Container : IRegisterOnlyContainer, IServiceCollection
    {
        private readonly Microsoft.Extensions.DependencyInjection.IServiceCollection _collection;
        private ServiceProvider? _serviceProvider;
        private readonly List<ServiceInitializer> _initializers = new();

        public Container()
            : this(new ServiceCollection())
        {
        }

        public Container(Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
        {
            _collection = serviceCollection;
        }

    }
}

#endif
