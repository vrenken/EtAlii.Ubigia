// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// This are the options for a LogicalContext instance. It provides all settings and extensions
    ///to facilitate configurable logical graph querying and traversal.
    /// </summary>
    public class LogicalOptions : FabricContextOptions, ILogicalOptions, IEditableLogicalOptions
    {
        /// <summary>
        /// Set this property to true to enable client-side caching. It makes sure that the immutable entries
        /// and relations are kept on the client.This reduces network traffic but requires more local memory.
        /// </summary>
        bool IEditableLogicalOptions.CachingEnabled { get => CachingEnabled; set => CachingEnabled = value; }
        public bool CachingEnabled { get; protected set; }

        public LogicalOptions(IConfigurationRoot configurationRoot)
            : base(configurationRoot)
        {
            CachingEnabled = true;
        }
    }
}
