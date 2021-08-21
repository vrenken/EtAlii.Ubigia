// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// This are the options for a LogicalContext instance. It provides all settings and extensions
    ///to facilitate configurable logical graph querying and traversal.
    /// </summary>
    public sealed class LogicalOptions : ILogicalOptions
    {
        public IConfigurationRoot ConfigurationRoot { get; }

        public IFabricContext FabricContext { get; private set; }

        public IExtension[] Extensions { get; private set; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get => Extensions; set => Extensions = value; }

        public LogicalOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;

            Extensions = Array.Empty<IExtension>();
        }

        public LogicalOptions UseFabricContext(IFabricContext fabricContext)
        {
            FabricContext = fabricContext ?? throw new ArgumentException("No fabric context specified", nameof(fabricContext));
            return this;
        }
    }
}
