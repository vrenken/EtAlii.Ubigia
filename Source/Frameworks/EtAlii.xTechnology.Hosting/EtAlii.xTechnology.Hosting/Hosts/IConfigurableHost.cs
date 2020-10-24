namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    public interface IConfigurableHost : IHost
    {
        IHostManager Manager { get; }
        event Action<IApplicationBuilder> ConfigureApplication;
        event Action<IWebHostBuilder> ConfigureHost;
        public event Action<KestrelServerOptions> ConfigureKestrel;
    }
}
