// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Hosting;

    public interface IComponentManager
    {
        void Start(IHostBuilder hostBuilder);
        void Stop();
    }
}