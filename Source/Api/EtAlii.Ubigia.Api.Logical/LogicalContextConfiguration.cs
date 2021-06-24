// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    /// <summary>
    /// This is the Configuration for a LogicalContext instance. It provides all settings and extensions
    ///to facilitate configurable logical graph querying and traversal.  
    /// </summary>
    public class LogicalContextConfiguration : FabricContextConfiguration, ILogicalContextConfiguration, IEditableLogicalContextConfiguration
    { 
        /// <summary>
        /// Set this property to true to enable client-side caching. It makes sure that the immutable entries
        /// and relations are kept on the client.This reduces network traffic but requires more local memory.    
        /// </summary>
        bool IEditableLogicalContextConfiguration.CachingEnabled { get => CachingEnabled; set => CachingEnabled = value; }
        public bool CachingEnabled { get; private set; }

        public LogicalContextConfiguration()
        {
            CachingEnabled = true;
        }
    }
}