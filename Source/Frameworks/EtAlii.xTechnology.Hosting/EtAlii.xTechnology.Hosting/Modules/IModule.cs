// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IModule : INotifyPropertyChanged
    {
		State State { get; }
        Status Status { get; }

        IModule[] Modules { get; }
        IService[] Services { get; }

        HostString HostString { get; }
        PathString PathString { get; }
        
        Task Start();
        Task Stop();

        void Setup(IHost host, ISystem system, IService[] services, IModule[] modules, IModule parentModule);
        void Initialize();
    }
}
