// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public interface IFabricContextOptions : IExtensible
    {
        /// <summary>
        /// The Connection that should be used to communicate with the backend.
        /// </summary>
        IDataConnection Connection { get; }

        /// <summary>
        /// The client configuration root that will be used to configure the fabric context.
        /// </summary>
        IConfigurationRoot ConfigurationRoot { get; }

        /// <summary>
        /// Set this property to true to enable client-side caching. It makes sure that the immutable entries
        /// and relations are kept on the client.This reduces network traffic but requires more local memory.
        /// </summary>
        bool CachingEnabled { get; }
    }
}
