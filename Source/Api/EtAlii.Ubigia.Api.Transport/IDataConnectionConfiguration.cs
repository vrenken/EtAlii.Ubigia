// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IDataConnectionConfiguration : IConfiguration
    {
        Uri Address { get; }
        string AccountName { get; }
        string Password { get; }
        string Space { get; }

        ITransportProvider TransportProvider { get; }
        Func<IDataConnection> FactoryExtension { get; }
    }
}