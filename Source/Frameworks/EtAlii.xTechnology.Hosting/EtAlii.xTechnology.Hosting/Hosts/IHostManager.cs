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

        bool ShouldOutputLog { get; set; }
        LogLevel LogLevel { get; set; }

        event Action<IApplicationBuilder> ConfigureApplication;
        event Action<IWebHostBuilder> ConfigureHost;
        event Action<KestrelServerOptions> ConfigureKestrel;

        Task Started();
        Task Starting();
        Task Stopped();
        Task Stopping();

        void Setup(ref ICommand[] commands, ref Status[] status, IHost host);
        void Initialize();
    }
}