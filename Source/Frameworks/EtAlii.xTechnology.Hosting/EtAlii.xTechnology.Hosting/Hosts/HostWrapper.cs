// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;

    public class HostWrapper : IHost
    {
        public IHost CurrentHost => _currentHost;
        private IHost _currentHost;

        public event PropertyChangedEventHandler PropertyChanged;

        public IHostOptions Options => _currentHost.Options;
        public State State => _currentHost.State;

        public IService[] Services => _currentHost.Services;

        public Status[] Status => _currentHost.Status;

        public ICommand[] Commands => _currentHost.Commands;

        event Action<IWebHostBuilder> IHost.ConfigureHost { add => _configureHost += value; remove => _configureHost -= value; }
        private Action<IWebHostBuilder> _configureHost;

        public HostWrapper(IHost host)
        {
            _currentHost = host;
            Wire();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        private void OnConfigureHost(IWebHostBuilder webHostBuilder)
        {
            _configureHost?.Invoke(webHostBuilder);
        }

        private void Wire()
        {
            _currentHost.PropertyChanged += OnPropertyChanged;
            _currentHost.ConfigureHost += OnConfigureHost;
        }

        private void Unwire()
        {
            _currentHost.PropertyChanged -= OnPropertyChanged;
            _currentHost.ConfigureHost -= OnConfigureHost;
        }

        public void Replace(IHost host)
        {
            Unwire();
            _currentHost = host;
            Wire();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Commands)));
            // No need to raise PropertyChanged for the State an Status as these will still be State.Shutdown and
            // not initialized yet.
        }
        public Task Start() => _currentHost.Start();
        public Task Stop() => _currentHost.Stop();

        public Task Shutdown() => _currentHost.Shutdown();

        public void Setup(ICommand[] commands) => _currentHost.Setup(commands);
    }
}
