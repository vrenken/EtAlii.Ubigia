// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Threading.Tasks;

    public interface IHost : INotifyPropertyChanged
    {
        IHostConfiguration Configuration { get; }
        Task Start();
        Task Stop();

        Task Shutdown();

        State State { get; }

        Status[] Status { get; }

        ICommand[] Commands { get; }

		ISystem[] Systems { get; }

        void Setup(ICommand[] commands, Status[] status);

        void Initialize();
    }
}
