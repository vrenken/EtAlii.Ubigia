// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class LogicalContextOptions : IExtensible
    {
        /// <summary>
        /// The host configuration root that will be used to configure the logical context.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <summary>
        /// The fabric that should be used by the logical context.
        /// </summary>
        public IFabricContext Fabric { get; private set; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get; set; }

        public LogicalContextOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            ((IExtensible)this).Extensions = Array.Empty<IExtension>();
        }

        /// <summary>
        /// Set the fabric that should be used by the logical context.
        /// </summary>
        public LogicalContextOptions Use(IFabricContext fabric)
        {
			Fabric = fabric ?? throw new ArgumentException("No fabric context specified", nameof(fabric));

            return this;
        }
    }
}
