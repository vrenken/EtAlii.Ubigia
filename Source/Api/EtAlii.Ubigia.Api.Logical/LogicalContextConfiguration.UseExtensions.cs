// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved. 
    /// </summary>
    public static class LogicalContextConfigurationUseExtensions
    {

        public static TLogicalContextConfiguration UseCaching<TLogicalContextConfiguration>(this TLogicalContextConfiguration configuration, bool cachingEnabled)
            where TLogicalContextConfiguration: LogicalContextConfiguration
        {
            ((IEditableLogicalContextConfiguration)configuration).CachingEnabled = cachingEnabled;
            return configuration;
        }
        
        
        public static TLogicalContextConfiguration Use<TLogicalContextConfiguration>(this TLogicalContextConfiguration configuration, LogicalContextConfiguration otherConfiguration)
            where TLogicalContextConfiguration: LogicalContextConfiguration
        {
            // ReSharper disable once RedundantCast
            configuration.Use((FabricContextConfiguration)otherConfiguration); // This cast is needed!

            ((IEditableLogicalContextConfiguration)configuration).CachingEnabled = otherConfiguration.CachingEnabled;
            return configuration;
        }
    }
}