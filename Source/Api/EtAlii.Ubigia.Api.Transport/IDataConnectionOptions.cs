// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using Microsoft.Extensions.Configuration;

    public interface IDataConnectionOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root used to instantiate the data connection.
        /// </summary>
        IConfiguration ConfigurationRoot { get; }

        Uri Address { get; }

        string AccountName { get; }

        string Password { get; }

        string Space { get; }

        ITransportProvider TransportProvider { get; }

        Func<IDataConnection> FactoryExtension { get; }
    }
}
