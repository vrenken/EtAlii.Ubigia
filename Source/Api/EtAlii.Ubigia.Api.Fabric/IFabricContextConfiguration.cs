// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
    using Microsoft.Extensions.Configuration;

    public interface IFabricContextConfiguration : IExtensible
    {
        /// <summary>
        /// The Connection that should be used to communicate with the backend.
        /// </summary>
        IDataConnection Connection { get; }

        IConfiguration ConfigurationRoot { get; }
        bool TraversalCachingEnabled { get; }
    }
}
