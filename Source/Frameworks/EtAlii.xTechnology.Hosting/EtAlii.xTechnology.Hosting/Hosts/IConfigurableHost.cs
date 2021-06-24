// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Logging;

    public interface IConfigurableHost : IHost
    {
        IHostManager Manager { get; }
        event Action<IApplicationBuilder> ConfigureApplication;
        event Action<IWebHostBuilder> ConfigureHost;
        event Action<KestrelServerOptions> ConfigureKestrel;
        event Action<ILoggingBuilder> ConfigureLogging;
    }
}
