// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Fabric;
    using Microsoft.Extensions.Configuration;

    public interface ILogicalContextOptions
    {
        /// <summary>
        /// The host configuration root that will be used to configure the logical context.
        /// </summary>
        IConfigurationRoot ConfigurationRoot { get; }

	    /// <summary>
	    /// The name of the Ubigia storage.
	    /// </summary>
        string Name { get; }

        /// <summary>
        /// The address (schema+host) at which the storage can be found.
        /// </summary>
	    Uri StorageAddress { get; }

        /// <summary>
        /// The fabric that should be used by the logical context.
        /// </summary>
        IFabricContext Fabric { get; }

        ILogicalContextOptions Use(string name, Uri storageAddress);

        ILogicalContextOptions Use(IFabricContext fabric);
    }
}
