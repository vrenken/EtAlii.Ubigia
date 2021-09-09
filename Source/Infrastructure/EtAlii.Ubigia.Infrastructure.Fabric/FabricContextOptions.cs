// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Persistence;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class FabricContextOptions : IExtensible
    {
        /// <summary>
        /// The host configuration root that will be used to configure the logical context.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        public IStorage Storage { get; private set; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get => _extensions; set => _extensions = value; }
        private IExtension[] _extensions;

        public FabricContextOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            _extensions = Array.Empty<IExtension>();
        }

        public FabricContextOptions Use(IStorage storage)
        {
            Storage = storage ?? throw new ArgumentException("No storage specified", nameof(storage));

            return this;
        }
    }
}
