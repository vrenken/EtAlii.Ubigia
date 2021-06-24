// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Logging;

    public interface IHostManager
    {
        IWebHost Host { get; }

        event Action<IApplicationBuilder> ConfigureApplication;
        event Action<IWebHostBuilder> ConfigureHost;
        event Action<KestrelServerOptions> ConfigureKestrel;
        event Action<ILoggingBuilder> ConfigureLogging;

        Task Started();
        Task Starting();
        Task Stopped();
        Task Stopping();

        void Setup(ref ICommand[] commands, IHost host);
        void Initialize();
    }
}
