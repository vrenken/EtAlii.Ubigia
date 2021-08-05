// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing options type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class LogicalContextOptionsUseExtensions
    {

        public static TLogicalContextOptions UseCaching<TLogicalContextOptions>(this TLogicalContextOptions options, bool cachingEnabled)
            where TLogicalContextOptions: LogicalContextOptions
        {
            ((IEditableLogicalContextOptions)options).CachingEnabled = cachingEnabled;
            return options;
        }


        public static TLogicalContextOptions Use<TLogicalContextOptions>(this TLogicalContextOptions options, LogicalContextOptions otherOptions)
            where TLogicalContextOptions: LogicalContextOptions
        {
            // ReSharper disable once RedundantCast
            options.Use((FabricContextOptions)otherOptions); // This cast is needed!

            ((IEditableLogicalContextOptions)options).CachingEnabled = otherOptions.CachingEnabled;
            return options;
        }
    }
}
