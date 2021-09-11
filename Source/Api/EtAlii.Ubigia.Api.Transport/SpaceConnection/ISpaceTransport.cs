// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
	using System;
	using System.Threading.Tasks;
    using EtAlii.xTechnology.MicroContainer;

    public interface ISpaceTransport
    {
        bool IsConnected { get; }

        Uri Address { get; }

        Task Start();

        Task Stop();

        IScaffolding[] CreateScaffolding(SpaceConnectionOptions spaceConnectionOptions);
    }
}
