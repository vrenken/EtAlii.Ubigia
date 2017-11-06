using System;

namespace EtAlii.xTechnology.Hosting
{
    public interface IHost
    {
        void Start();
        void Stop();

        void Shutdown();

        HostStatus Status { get; }

        event Action<HostStatus> StatusChanged;

        IHostCommand[] Commands { get; }
        void Initialize(IHostCommand[] commands);
    }
}
