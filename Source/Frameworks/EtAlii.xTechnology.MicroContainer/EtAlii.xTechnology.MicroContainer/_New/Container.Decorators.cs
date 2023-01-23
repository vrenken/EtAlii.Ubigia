// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This container is build to be simple and pure. We don't want to assign too much 'lifetime' responsibilities
    /// to our DI framework. Most lifetime behavior can be solved way easier without a container.
    /// </summary>
    public partial class Container
    {
        /// <inheritdoc />
        public void RegisterDecorator<TService, TDecorator>()
            where TService : class
            where TDecorator : class, TService
        {
            _collection.RegisterDecorator<TService, TDecorator>();
        }
    }
}

#endif
