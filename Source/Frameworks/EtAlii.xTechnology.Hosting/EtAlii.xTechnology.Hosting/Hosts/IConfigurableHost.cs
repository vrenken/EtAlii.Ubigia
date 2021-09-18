// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    public interface IConfigurableHost : IHost
    {
        IHostManager Manager { get; }
        event Action<IApplicationBuilder, IWebHostEnvironment> ConfigureApplication;
        event Action<IWebHostBuilder> ConfigureHost;
        event Action<KestrelServerOptions> ConfigureKestrel;
    }
}
