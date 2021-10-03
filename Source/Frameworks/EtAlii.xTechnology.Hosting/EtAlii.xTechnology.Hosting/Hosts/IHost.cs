// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;

    public interface IHost : INotifyPropertyChanged
    {
        IHostOptions Options { get; }

        State State { get; }

        Status[] Status { get; }

        ICommand[] Commands { get; }

        void Setup(ICommand[] commands);

        Task Start();

        Task Stop();

        Task Shutdown();

        internal event Action<IWebHostBuilder> ConfigureHost;
    }
}
