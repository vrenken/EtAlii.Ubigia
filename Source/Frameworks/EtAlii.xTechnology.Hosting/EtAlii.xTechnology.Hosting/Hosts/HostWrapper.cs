// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Logging;

    public class HostWrapper : IConfigurableHost
    {
        private IConfigurableHost _host;

        public IHostManager Manager => _host.Manager;

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<IApplicationBuilder> ConfigureApplication;
        public event Action<IWebHostBuilder> ConfigureHost;
        public event Action<KestrelServerOptions> ConfigureKestrel;
        public event Action<ILoggingBuilder> ConfigureLogging;

        public IHostConfiguration Configuration => _host.Configuration;
        public State State => _host.State;

        public Status[] Status => _host.Status;

        public ICommand[] Commands => _host.Commands;

        public ISystem[] Systems => _host.Systems;

        public HostWrapper(IHost host)
        {
            _host = (IConfigurableHost)host;
            Wire();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
        private void OnConfigureApplication(IApplicationBuilder builder) => ConfigureApplication?.Invoke(builder);
        private void OnConfigureHost(IWebHostBuilder builder) => ConfigureHost?.Invoke(builder);
        private void OnConfigureKestrel(KestrelServerOptions options) => ConfigureKestrel?.Invoke(options);
        private void OnConfigureLogging(ILoggingBuilder builder) => ConfigureLogging?.Invoke(builder);

        private void Wire()
        {
            _host.PropertyChanged += OnPropertyChanged;
            _host.ConfigureApplication += OnConfigureApplication;
            _host.ConfigureHost += OnConfigureHost;
            _host.ConfigureKestrel += OnConfigureKestrel;
            _host.ConfigureLogging += OnConfigureLogging;
        }

        private void Unwire()
        {
            _host.PropertyChanged -= OnPropertyChanged;
            _host.ConfigureApplication -= OnConfigureApplication;
            _host.ConfigureHost -= OnConfigureHost;
            _host.ConfigureKestrel -= OnConfigureKestrel;
            _host.ConfigureLogging -= OnConfigureLogging;
        }

        public void Replace(IHost host)
        {
            Unwire();
            _host = (IConfigurableHost)host;
            Wire();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Commands)));
            // No need to raise PropertyChanged for the State an Status as these will still be State.Shutdown and
            // not initialized yet.
        }
        public Task Start() => _host.Start();
        public Task Stop() => _host.Stop();

        public Task Shutdown() => _host.Shutdown();

        public void Setup(ICommand[] commands, Status[] status) => _host.Setup(commands, status);

        public void Initialize() => _host.Initialize();
    }
}
