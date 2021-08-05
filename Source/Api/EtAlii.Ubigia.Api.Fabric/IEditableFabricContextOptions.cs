// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public interface IEditableFabricContextOptions
    {
        /// <summary>
        /// Gets or sets the Connection that should be used to communicate with the backend.
        /// </summary>
        IDataConnection Connection { get; set; }
        bool TraversalCachingEnabled { get; set; }
    }
}
