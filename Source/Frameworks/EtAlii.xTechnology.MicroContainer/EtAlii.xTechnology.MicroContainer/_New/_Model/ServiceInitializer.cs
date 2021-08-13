// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if !USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    internal class ServiceInitializer<TService> : ServiceInitializer
    {
        private readonly Action<TService> _initializer;

        private bool _isInitialized;
        public ServiceInitializer(Action<TService> initializer)
        {
            _initializer = initializer;
        }

        public override void Initialize(ServiceProvider serviceProvider)
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                var instance = serviceProvider.GetService<TService>();
                _initializer(instance!);
            }
        }
    }

    internal abstract class ServiceInitializer
    {
        public abstract void Initialize(ServiceProvider serviceProvider);
    }
}

#endif
