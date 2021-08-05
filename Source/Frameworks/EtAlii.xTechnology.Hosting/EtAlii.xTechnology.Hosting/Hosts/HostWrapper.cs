// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    public class HostWrapper : IConfigurableHost
    {
        public IHost CurrentHost => _currentHost;
        private IConfigurableHost _currentHost;

        public IHostManager Manager => _currentHost.Manager;

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<IApplicationBuilder> ConfigureApplication;
        public event Action<IWebHostBuilder> ConfigureHost;
        public event Action<KestrelServerOptions> ConfigureKestrel;
        public IHostOptions Options => _currentHost.Options;
        public State State => _currentHost.State;

        public Status[] Status => _currentHost.Status;

        public ICommand[] Commands => _currentHost.Commands;

        public ISystem[] Systems => _currentHost.Systems;

        public HostWrapper(IHost host)
        {
            _currentHost = (IConfigurableHost)host;
            Wire();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
        private void OnConfigureApplication(IApplicationBuilder builder) => ConfigureApplication?.Invoke(builder);
        private void OnConfigureHost(IWebHostBuilder builder) => ConfigureHost?.Invoke(builder);
        private void OnConfigureKestrel(KestrelServerOptions options) => ConfigureKestrel?.Invoke(options);
        private void Wire()
        {
            _currentHost.PropertyChanged += OnPropertyChanged;
            _currentHost.ConfigureApplication += OnConfigureApplication;
            _currentHost.ConfigureHost += OnConfigureHost;
            _currentHost.ConfigureKestrel += OnConfigureKestrel;
        }

        private void Unwire()
        {
            _currentHost.PropertyChanged -= OnPropertyChanged;
            _currentHost.ConfigureApplication -= OnConfigureApplication;
            _currentHost.ConfigureHost -= OnConfigureHost;
            _currentHost.ConfigureKestrel -= OnConfigureKestrel;
        }

        public void Replace(IHost host)
        {
            Unwire();
            _currentHost = (IConfigurableHost)host;
            Wire();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Commands)));
            // No need to raise PropertyChanged for the State an Status as these will still be State.Shutdown and
            // not initialized yet.
        }
        public Task Start() => _currentHost.Start();
        public Task Stop() => _currentHost.Stop();

        public Task Shutdown() => _currentHost.Shutdown();

        public void Setup(ICommand[] commands, Status[] status) => _currentHost.Setup(commands, status);

        public void Initialize() => _currentHost.Initialize();
    }
}
