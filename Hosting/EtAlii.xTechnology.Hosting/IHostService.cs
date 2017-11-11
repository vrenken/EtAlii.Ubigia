using System;

namespace EtAlii.xTechnology.Hosting
{
    public interface IHostService
    {
        //string Name { get; }
        //string Description { get; }
        HostStatus Status { get; } 

        void Start();
        void Stop();
    }
}
