// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
	using System;
	using EtAlii.Ubigia.Infrastructure.Fabric;
    using Microsoft.Extensions.Configuration;

    // TODO: Remove this interface.
    public interface ILogicalContextOptions
    {
        IConfigurationRoot ConfigurationRoot { get; }

        string Name { get; }

	    Uri StorageAddress { get; }

        IFabricContext Fabric { get; }

        LogicalContextOptions Use(string name, Uri storageAddress);

        LogicalContextOptions Use(IFabricContext fabric);
    }
}
