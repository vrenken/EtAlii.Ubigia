// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public interface IStorageConnectionOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root used to instantiate this ManagementConnection.
        /// </summary>
        IConfigurationRoot ConfigurationRoot { get; }

        /// <summary>
        /// The transport that will be used to instantiate this ManagementConnection.
        /// </summary>
        IStorageTransport Transport { get; }
    }
}
