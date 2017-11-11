using System;

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    public interface IHost : INotifyPropertyChanged
    {
        void Start();
        void Stop();

        void Shutdown();

        HostState State { get; }

        HostStatus[] Status { get; }

        IHostCommand[] Commands { get; }
        void Initialize(IHostCommand[] commands, HostStatus[] status);
    }
}
