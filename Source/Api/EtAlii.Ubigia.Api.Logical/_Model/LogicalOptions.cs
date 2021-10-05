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
    public sealed class LogicalOptions : IExtensible
    {
        public IConfigurationRoot ConfigurationRoot { get; }

        public IFabricContext FabricContext => _fabricContext.Value;
        private Lazy<IFabricContext> _fabricContext;

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get; set; }

        public LogicalOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;

            ((IExtensible)this).Extensions = new IExtension[] { new CommonLogicalExtension(this) };
        }

        public LogicalOptions UseFabricOptions(FabricOptions fabricOptions)
        {
            if (fabricOptions == null)
            {
                throw new ArgumentNullException(nameof(fabricOptions));
            }

            _fabricContext = new Lazy<IFabricContext>(() => Factory.Create<IFabricContext>(fabricOptions));
            return this;
        }
    }
}
