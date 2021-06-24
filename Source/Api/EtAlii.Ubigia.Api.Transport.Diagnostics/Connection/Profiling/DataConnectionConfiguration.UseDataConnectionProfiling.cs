// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    public static class DataConnectionConfigurationUseDataConnectionProfiling
    {
        public static TDataConnectionConfiguration UseDataConnectionProfiling<TDataConnectionConfiguration>(this TDataConnectionConfiguration configuration)
            where TDataConnectionConfiguration : DataConnectionConfiguration
        {
            configuration.Use(new IDataConnectionExtension[]
            {
                new ProfilingDataConnectionExtension(),
            });

            return configuration;
        }
    }
}