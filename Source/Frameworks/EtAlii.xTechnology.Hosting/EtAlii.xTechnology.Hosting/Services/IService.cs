// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IService : INotifyPropertyChanged
    {
        State State { get; }
        Status Status { get; }

        PathString PathString { get; }

        HostString HostString { get; }
        Task Start();
        Task Stop();

        void Setup(IHost host, ISystem system, IModule parentModule);
        void Initialize();
    }
}
