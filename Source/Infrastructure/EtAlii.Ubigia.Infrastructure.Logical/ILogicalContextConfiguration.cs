// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Fabric;

	public interface ILogicalContextConfiguration
    {
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
        
        ILogicalContextConfiguration Use(string name, Uri storageAddress);

        ILogicalContextConfiguration Use(IFabricContext fabric);
    }
}