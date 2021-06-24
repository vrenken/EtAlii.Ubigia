// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.Extensions.Hosting;

    public interface IComponent
    {
        void Start(IHostBuilder hostBuilder);
        void Stop();

        bool TryGetService(Type serviceType, out object serviceInstance);
    }
}