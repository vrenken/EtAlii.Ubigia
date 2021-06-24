// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Threading.Tasks;

    public interface ISystem : INotifyPropertyChanged
    {
        State State { get; }

        Status Status { get; }

        ICommand[] Commands { get; }

        IService[] Services { get; }
        IModule[] Modules { get; }

        Task Start();
        Task Stop();

        void Setup(IHost host, IService[] services, IModule[] modules);
        void Initialize();
    }
}
