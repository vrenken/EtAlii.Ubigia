// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing options type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class LogicalOptionsUseExtensions
    {

        public static TLogicalOptions UseCaching<TLogicalOptions>(this TLogicalOptions options, bool cachingEnabled)
            where TLogicalOptions: LogicalOptions
        {
            ((IEditableLogicalOptions)options).CachingEnabled = cachingEnabled;
            return options;
        }


        public static TLogicalOptions Use<TLogicalOptions>(this TLogicalOptions options, LogicalOptions otherOptions)
            where TLogicalOptions: LogicalOptions
        {
            // ReSharper disable once RedundantCast
            options.Use((FabricContextOptions)otherOptions); // This cast is needed!

            ((IEditableLogicalOptions)options).CachingEnabled = otherOptions.CachingEnabled;
            return options;
        }
    }
}
