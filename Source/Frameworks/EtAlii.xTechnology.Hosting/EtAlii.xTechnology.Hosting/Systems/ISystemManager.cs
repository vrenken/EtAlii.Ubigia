// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Threading.Tasks;

    public interface ISystemManager
    {
	    ISystem[] Systems { get; }

        void Setup(ISystem[] systems);
        Task Start();
        Task Stop();
    }
}
